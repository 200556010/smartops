using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Category;
using Model.Product;
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
    /// <summary>
    /// Product Service Implementation.
    /// </summary>
    /// <seealso cref="Service.Contract.IProductService" />
    public class ProductService : IProductService
    {
        #region || *** Private Variable *** ||
        private readonly ILogger<ProductService> _logger;
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
        public ProductService(ILogger<ProductService> logger, SmartOpsContext context, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
        }
        #endregion

        #region || *** Public Method *** ||
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Response> DeleteAsync(string id)
        {
            var response = new Response();
            try
            {
                var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

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

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ResponseData<ProductModel>> GetAsync(string id)
        {
            var response = new ResponseData<ProductModel>();
            try
            {
                var entity = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (entity != null)
                {
                    var model = new ProductModel()
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
                        CategoryId = entity.CategoryId,
                        Price = entity.Price,
                        Sku = entity.Sku,
                        StockQuantity = entity.StockQuantity,
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

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="dtParam">The dt parameter.</param>
        /// <returns></returns>
        public async Task<ResponseData<DataTableList<ProductListModel>>> GetListAsync(DTParameters dtParam)
        {
            var response = new ResponseData<DataTableList<ProductListModel>>();
            try
            {
                var query = _context.Products.Include(x=> x.Category).Where(x => x.IsDeleted != true)
                    .Select(x => new ProductListModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        CreatedOn = x.CreatedOn,
                        CategoryName = x.Category.Name,
                        StockQuantity = x.StockQuantity,
                        Sku = x.Sku,
                        Price = x.Price,

                    }).OrderBy(o => o.CreatedOn).AsQueryable();

                if (dtParam.Columns != null)
                {
                    var sbPlanName = dtParam.Columns.FirstOrDefault(x => x.Name == nameof(ProductListModel.Name))?.Search?.Value?.Trim();
                    var sbDescription = dtParam.Columns.FirstOrDefault(x => x.Name == nameof(ProductListModel.Description))?.Search?.Value?.Trim();

                    if (!string.IsNullOrEmpty(sbPlanName))
                        query = query.Where(r => !string.IsNullOrEmpty(r.Name) && r.Name.ToLower().Contains(sbPlanName.ToLower()));

                    if (!string.IsNullOrEmpty(sbDescription))
                        query = query.Where(r => !string.IsNullOrEmpty(r.Description) && r.Description.ToLower().Contains(sbDescription.ToLower()));

                }

                if (dtParam.Order != null && dtParam.Order.Length > 0 && dtParam.Order[0].Column.ToString() != "0")
                {
                    query = DataTable.OrderBy(query, dtParam, ProductSort);
                }

                var result = new PagedResult<ProductListModel>();

                if (string.IsNullOrEmpty(dtParam.exportColumns))
                {
                    result = await query.GetPagedAsync(dtParam.PageIndex == 0 ? 1 : dtParam.PageIndex + 1, dtParam.Length);
                }
                else
                {
                    result.Results = query.ToList();
                }
                response.Data = new DataTableList<ProductListModel>()
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

        /// <summary>
        /// Upserts the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Response> UpsertAsync(ProductModel model)
        {
            var response = new Response();
            try
            {
                var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (entity != null)
                {
                    entity.UpdatedBy = _currentUserService.UserId;
                    entity.UpdatedOn = DateTime.UtcNow;
                    entity.Description = model.Description;
                    entity.Status = model.Status;
                    entity.Name = model.Name;
                    entity.IsDeleted = false;
                    entity.CategoryId = model.CategoryId;
                    entity.Price = model.Price;
                    entity.Sku = model.Sku;
                    entity.StockQuantity = model.StockQuantity;

                    await _context.SaveChangesAsync();

                    response.Success = true;
                    response.Message = "Category updated successfully.";
                    return response;
                }
                else
                {
                    var data = new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = model.Description,
                        Status = model.Status,
                        Name = model.Name,
                        IsDeleted = false,
                        CreatedBy = _currentUserService.UserId,
                        CreatedOn = DateTime.UtcNow,
                        CategoryId = model.CategoryId,
                        Price = model.Price,
                        Sku = model.Sku,
                        StockQuantity = model.StockQuantity,

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
        #endregion

        #region || *** Private Methods *** ||

        /// <summary>
        /// Taxes the sort.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        private static IOrderedQueryable<ProductListModel> ProductSort(IOrderedQueryable<ProductListModel> query, string sortOrder)
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
