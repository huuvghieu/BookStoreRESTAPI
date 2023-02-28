using System;
using System.Collections.Generic;

namespace BookStore.Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Role { get; set; }

    public virtual ICollection<OrderBook> OrderBooks { get; } = new List<OrderBook>();
}
