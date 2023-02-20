using System;
using System.Collections.Generic;

namespace BookStore.Data.Models;

public partial class OrderBook
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime OrderReturnDate { get; set; }

    public int? Status { get; set; }

    public int UserId { get; set; }

    public int? TotalPrice { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}
