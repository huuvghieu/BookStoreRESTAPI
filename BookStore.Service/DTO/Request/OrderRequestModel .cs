
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.RequestModels
{
    public class OrderRequestModel
    {

        public DateTime? OrderDate { get; set; } = null;
        public DateTime? OrderReturnDate { get; set; } = null;
        public int Status { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
