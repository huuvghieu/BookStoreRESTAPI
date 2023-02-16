using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Models;
using NTQ.Sdk.Core.ViewModels;
using BookStore.Service.DTO.Response;

namespace DataAcess.ResponseModels
{
    public class BookReponseModel:SortModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string? BookImg { get; set; }
        public int CurrentQuantity { get; set; }
        public string BookDetail { get; set; }
        public int CateID { get; set; }
        public CategoryResponse Cate { get; set; }
        public int Price { get; set; }
    }
}
