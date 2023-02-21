
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Models;
using NTQ.Sdk.Core.ViewModels;
using NTQ.Sdk.Core.Attributes;
using BookStore.Service.DTO.Response;

namespace DataAcess.ResponseModels
{
    public class OrderReponseModel:SortModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Skip]
        public DateTime OrderDate { get; set; }
        [Skip]
        public DateTime OrderReturnDate { get; set; }
        [IntAttribute]
        public int? Status { get; set; } 
        public UserResponse User { get; set; } 
        public List<OrderDetailReponseModel> OrderDetail { get; set; }
    }
}
