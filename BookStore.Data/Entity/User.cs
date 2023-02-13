using System;
using System.Collections.Generic;

namespace BookStore.Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Address { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<OrderBook> OrderBooks { get; } = new List<OrderBook>();
}
