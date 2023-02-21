using DataAcess.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Request
{
    public class OrderCreateRequestModel
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime OrderReturnDate { get; set; } = DateTime.Now.AddMonths(2);
        public int? UserId { get; set; } = null;
        public virtual ICollection<OrderDetailRequestModel> OrderDetails { get; set; }
    }
}
