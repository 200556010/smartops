using Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Category;
using Service.Contract;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.Contract;
using static Data.Helpers.PaginationExtensionMethod;

namespace Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        #region || *** Private Variable *** ||
        private readonly ILogger<CategoryService> _logger;
        private readonly SmartOpsContext _context;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region || *** CTOR *** ||
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        /// <param name="currentUserService">The current user service.</param>
        /// <param name="messagesResource">The messages resource.</param>
        public CategoryService(ILogger<CategoryService> logger, SmartOpsContext context, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
        }
        #endregion

        #region || *** Public Method *** ||
        public async Task<Response> DeleteAsync(string id)
        {
            var response = new Response();
            try
            {
                var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.UpdatedBy = _currentUserService.UserId;
                    entity.UpdatedOn = DateTime.UtcNow;
                    await _context.SaveChangesAsync();

                    response.Success = true;
                    response.Message = "Category deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Category not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "Something went wrong.";
            }
            return response;
        }

        public async Task<ResponseData<CategoryModel>> GetAsync(string id)
        {
            var response = new ResponseData<CategoryModel>();
            try
            {
                var entity = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (entity != null)
                {
                    var model = new CategoryModel()
                    {
                        Id = entity.Id,
                        CreatedBy = entity.CreatedBy,
                        CreatedOn = entity.CreatedOn,
                        Description = entity.Description,
                        IsDeleted = entity.IsDeleted,
                        Name = entity.Name,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedOn = entity.UpdatedOn,
                        Status = entity.Status,
                    };
                    response.Data = model;
                    response.Success = true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "Something went wrong.";
            }
            return response;
        }

        public async Task<ResponseData<DataTableList<CategoryListModel>>> GetListAsync(DTParameters dtParam)
        {
            var response = new ResponseData<DataTableList<CategoryListModel>>();
            try
            {
                var query = _context.Categories.Where(x => x.IsDeleted != true)
                    .Select(x => new CategoryListModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        CreatedOn = x.CreatedOn,
                    }).OrderBy(o => o.CreatedOn).AsQueryable();

                if (dtParam.Columns != null)
                {
                    var sbPlanName = dtParam.Columns.FirstOrDefault(x => x.Name == nameof(CategoryListModel.Name))?.Search?.Value?.Trim();
                    var sbDescription = dtParam.Columns.FirstOrDefault(x => x.Name == nameof(CategoryListModel.Description))?.Search?.Value?.Trim();

                    if (!string.IsNullOrEmpty(sbPlanName))
                        query = query.Where(r => !string.IsNullOrEmpty(r.Name) && r.Name.ToLower().Contains(sbPlanName.ToLower()));

                    if (!string.IsNullOrEmpty(sbDescription))
                        query = query.Where(r => !string.IsNullOrEmpty(r.Description) && r.Description.ToLower().Contains(sbDescription.ToLower()));

                }

                if (dtParam.Order != null && dtParam.Order.Length > 0 && dtParam.Order[0].Column.ToString() != "0")
                {
                    query = DataTable.OrderBy(query, dtParam, CategorySort);
                }

                var result = new PagedResult<CategoryListModel>();

                if (string.IsNullOrEmpty(dtParam.exportColumns))
                {
                    result = await query.GetPagedAsync(dtParam.PageIndex == 0 ? 1 : dtParam.PageIndex + 1, dtParam.Length);
                }
                else
                {
                    result.Results = query.ToList();
                }
                response.Data = new DataTableList<CategoryListModel>()
                {
                    draw = dtParam.Draw,
                    recordsFiltered = result.RowCount,
                    recordsTotal = result.RowCount,
                    data = result.Results.AsEnumerable()
                };

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "Something went wrong.";
            }
            return response;
        }

        public async Task<Response> UpsertAsync(CategoryModel model)
        {
            var response = new Response();
            try
            {
                var entity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (entity != null)
                {
                    entity.UpdatedBy = _currentUserService.UserId;
                    entity.UpdatedOn = DateTime.UtcNow;
                    entity.Description = model.Description;
                    entity.Status = model.Status;
                    entity.Name = model.Name;
                    entity.IsDeleted = false;

                    await _context.SaveChangesAsync();

                    response.Success = true;
                    response.Message = "Category updated successfully.";
                    return response;
                }
                else
                {
                    var data = new Category()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = model.Description,
                        Status = model.Status,
                        Name = model.Name,
                        IsDeleted = false,
                        CreatedBy = _currentUserService.UserId,
                        CreatedOn = DateTime.UtcNow,
                    };
                    _context.Add(data);

                    await _context.SaveChangesAsync();

                    response.Success = true;
                    response.Message = "Category added successfully.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "Something went wrong.";
            }
            return response;
        }

        public List<SelectListItem> GetCategoryList()
        {
            var query = _context.Categories.Where(w => w.IsDeleted != true);
            var categoryList = query.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return categoryList;
        }
        #endregion

        #region || *** Private Methods *** ||

        /// <summary>
        /// Taxes the sort.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        private static IOrderedQueryable<CategoryListModel> CategorySort(IOrderedQueryable<CategoryListModel> query, string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
            {
                query = sortOrder switch
                {
                    "name" => query.ThenBy(x => x.Name),
                    "name DESC" => query.ThenByDescending(x => x.Name),
                    "description" => query.ThenBy(x => x.Description),
                    "description DESC" => query.ThenByDescending(x => x.Description),
                    _ => query.ThenByDescending(x => x.CreatedOn),
                };
            }
            return query;
        }

        #endregion
    }
}
