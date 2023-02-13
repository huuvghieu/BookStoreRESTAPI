
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.RequestModels
{
    public class OrderCreateRequestModel
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
