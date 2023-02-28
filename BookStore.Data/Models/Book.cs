using System;
using System.Collections.Generic;

namespace BookStore.Data.Models;

public partial class Book
{
    public int BookId { get; set; }

    public double Price { get; set; }

    public string BookImg { get; set; } = null!;

    public string BookName { get; set; } = null!;

    public int CurrentQuantity { get; set; }

    public string? BookDetail { get; set; }

    public int? CateId { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
