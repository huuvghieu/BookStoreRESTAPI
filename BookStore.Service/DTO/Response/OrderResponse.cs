using BookStore.Data.Models;
using NTQ.Sdk.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Response
{
    public class OrderResponse : SortModel
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime OrderReturnDate { get; set; }

        public int? Status { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
    }
}
