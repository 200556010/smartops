using Microsoft.EntityFrameworkCore;

namespace Data.Helpers
{
    /// <summary>
    /// Pagination Extension Method
    /// </summary>
    public static class PaginationExtensionMethod
    {
        /// <summary>
        /// Gets the paged asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">
        /// Page number should be always greater then {minPageSize}.")
        /// or
        /// Page size should be always between {minPageSize + 1} to {maxPageSize} inclusive.");
        /// </exception>
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();

            if (pageSize == -1)
            {
                result.CurrentPage = page;
                result.RowCount = query.Count();
                result.PageSize = result.RowCount;
                result.PageCount = 1;
                result.Results = query.ToList();
                return result;
            }

            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = await query.CountAsync();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        /// <summary>
        /// Paged Result Class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <seealso cref="Service.Helper.PagedResultBase" />
        public class PagedResult<T> : PagedResultBase where T : class
        {
            public IList<T> Results { get; set; }

            public PagedResult()
            {
                Results = new List<T>();
            }
        }

        /// <summary>
        /// Paged Result Base
        /// </summary>
        public abstract class PagedResultBase
        {
            public int CurrentPage { get; set; }
            public int PageCount { get; set; }
            public int PageSize { get; set; }
            public int RowCount { get; set; }

            public int FirstRowOnPage
            {
                get { return (CurrentPage - 1) * PageSize + 1; }
            }

            public int LastRowOnPage
            {
                get { return Math.Min(CurrentPage * PageSize, RowCount); }
            }
        }
    }
}
