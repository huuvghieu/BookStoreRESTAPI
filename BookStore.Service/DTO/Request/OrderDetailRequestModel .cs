
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
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
