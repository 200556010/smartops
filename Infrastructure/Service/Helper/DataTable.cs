namespace Service.Helper
{
    /// <summary>
    /// Data Table Parameters
    /// </summary>
    public class DTParameters
    {
        private int _pageIndex;

        public int PageIndex
        {
            get
            {
                if (Length == 0)
                {
                    return 0;
                }

                return Start / Length;
            }
            set
            {
                _pageIndex = value;
                Start = _pageIndex * Length; // Update Start based on PageIndex and Length
            }
        }

        public int Draw { get; set; }

        public DTColumn[]? Columns { get; set; }

        public DTOrder[]? Order { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public DTSearch? Search { get; set; }

        public string? exportColumns { get; set; }

        public string SortOrder => (Columns != null && Order != null && Order.Length != 0) ? (Columns[Order[0].Column].Data + ((Order[0].Dir == DTOrderDir.DESC) ? (" " + Order[0].Dir) : string.Empty)) : null;
    }

    /// <summary>
    /// Data Table Columns
    /// </summary>
    public class DTColumn
    {
        public string? Data { get; set; }

        public string? Name { get; set; }

        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public DTSearch? Search { get; set; }
    }

    /// <summary>
    /// Data Table Order
    /// </summary>
    public class DTOrder
    {
        public int Column { get; set; }

        public DTOrderDir Dir { get; set; }
    }

    /// <summary>
    /// Data Table Search
    /// </summary>
    public class DTSearch
    {
        public string? Value { get; set; }

        public bool Regex { get; set; }
    }

    /// <summary>
    /// Data Table Order Direction 
    /// </summary>
    public enum DTOrderDir
    {
        ASC,
        DESC
    }

    /// <summary>
    /// Data Table List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTableList<T>
    {
        public int draw { get; set; }
        public long recordsTotal { get; set; }
        public long recordsFiltered { get; set; }
        public IEnumerable<T>? data { get; set; }
    }

    /// <summary>
    /// Data Table 
    /// </summary>
    public class DataTable
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> queryTable, DTParameters param, Func<IOrderedQueryable<TEntity>, string, IOrderedQueryable<TEntity>> orderCall)
        {
            var query = queryTable.OrderBy(p => 0);
            if (param.Order != null && param.Order.Length > 0)
            {
                foreach (var item in param.Order)
                {
                    var columnOrderKey = param?.Columns?[item.Column].Data + (item.Dir == DTOrderDir.DESC ? " " + item.Dir : string.Empty);
                    query = orderCall(query, columnOrderKey);
                }
            }
            queryTable = query;
            return queryTable;
        }
    }
}
