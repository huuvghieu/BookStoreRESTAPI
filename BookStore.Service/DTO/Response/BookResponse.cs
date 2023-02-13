using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Response
{
    public class BookResponse
    {
        public int BookId { get; set; }

        public string? BookName { get; set; }

        public string? BookImg { get; set; }

        public int CurrentQuantity { get; set; }

        public string? BookDetail { get; set; }

        public int CateId { get; set; }

        public int Price { get; set; }

        public virtual Category Cate { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
    }
}
