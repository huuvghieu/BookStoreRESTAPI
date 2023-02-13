using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Response
{
    public class OrderDetailResponse
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
        public virtual Book Book { get; set; }

        public virtual OrderBook Order { get; set; }
    }
}
