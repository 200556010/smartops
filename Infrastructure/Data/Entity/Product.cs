using System;
using System.Collections.Generic;

namespace Data.Entity;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? Sku { get; set; }

    public string? CategoryId { get; set; }

    public short? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual Category? Category { get; set; }
}
