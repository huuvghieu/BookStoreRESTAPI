using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Models;

namespace DataAcess.ResponseModels
{
    public class OrderDetailReponseModel
    {
        public string BookName { get; set; } = null;
        public string? BookImg { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ReturnedQuantity { get; set; }
    }
}
