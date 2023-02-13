using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Request
{
    public class OrderRequest
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime OrderReturnDate { get; set; } = DateTime.Now.AddDays(60);

        public int? Status { get; set; }

        public int UserId { get; set; }

        public  ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
    }
}
