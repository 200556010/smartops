using Data.Entity;
using Model.Category;
using Model.Product;
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
    public interface IProductService
    {
        /// <summary>
        /// Upserts the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<Response> UpsertAsync(ProductModel model);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ResponseData<ProductModel>> GetAsync(string id);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="dtParam">The dt parameter.</param>
        /// <returns></returns>
        Task<ResponseData<DataTableList<ProductListModel>>> GetListAsync(DTParameters dtParam);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Response> DeleteAsync(string id);
    }
}
