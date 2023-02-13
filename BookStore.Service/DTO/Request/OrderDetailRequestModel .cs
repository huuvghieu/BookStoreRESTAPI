
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.RequestModels
{
    public class OrderDetailRequestModel
    {
        public int OrderId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
