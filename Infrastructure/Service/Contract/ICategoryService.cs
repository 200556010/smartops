using Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Category;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Contract
{
    /// <summary>
    /// Category Service Contract.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Upserts the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> UpsertAsync(CategoryModel model);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ResponseData<CategoryModel>> GetAsync(string id);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="dtParam">The dt parameter.</param>
        /// <returns></returns>
        Task<ResponseData<DataTableList<CategoryListModel>>> GetListAsync(DTParameters dtParam);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Response> DeleteAsync(string id);

        /// <summary>
        /// Gets the category list.
        /// </summary>
        /// <returns></returns>
        List<SelectListItem> GetCategoryList();
    }
}
