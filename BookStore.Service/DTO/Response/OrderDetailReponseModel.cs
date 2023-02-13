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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }
        [ForeignKey("OrderBook")]
        public int OrderId { get; set; }
        public OrderBook OrderBook { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
