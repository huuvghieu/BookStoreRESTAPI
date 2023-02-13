using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Request
{
    public class ReturnOrderRequest
    {
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }

    }
}
