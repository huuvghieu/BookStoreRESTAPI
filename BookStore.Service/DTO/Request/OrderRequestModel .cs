
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
        public DateTime? OrderDate { get; set; } = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
        public DateTime? OrderReturnDate { get; set; } =(DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
        public int? Status { get; set; }=null;
        [ForeignKey("User")]
        public int? UserId { get; set; } = null;
        public double? TotalPrice { get; set; } = null;
    }
}
