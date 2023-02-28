using System;
using System.Collections.Generic;

namespace BookStore.Data.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public int ReturnedQuantity { get; set; }

    public double Price { get; set; }

    public string BookName { get; set; } = null!;

    public virtual Book? Book { get; set; }

    public virtual OrderBook? Order { get; set; }
}
