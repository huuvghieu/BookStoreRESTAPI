using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Models;
using NTQ.Sdk.Core.ViewModels;
using NTQ.Sdk.Core.Attributes;

namespace DataAcess.ResponseModels
{
    public class OrderReponseModel:SortModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderReturnDate { get; set; }
        public int Status { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderDetailReponseModel> OrderDetail { get; set; }
    }
}
