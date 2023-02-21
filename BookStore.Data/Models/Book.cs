using System;
using System.Collections.Generic;

namespace BookStore.Data.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? BookName { get; set; }

    public string? BookImg { get; set; }

    public int CurrentQuantity { get; set; }

    public string? BookDetail { get; set; }

    public int CateId { get; set; }

    public double Price { get; set; }

    public virtual Category Cate { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
