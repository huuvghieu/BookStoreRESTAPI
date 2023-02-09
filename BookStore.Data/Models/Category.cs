using System;
using System.Collections.Generic;

namespace BookStore.Data.Models;

public partial class Category
{
    public int CateId { get; set; }

    public string? CateName { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
