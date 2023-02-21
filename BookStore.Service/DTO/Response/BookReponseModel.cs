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
using NTQ.Sdk.Core.Attributes;
using System.Text.Json.Serialization;

namespace DataAcess.ResponseModels
{
    public class BookReponseModel:SortModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [StringAttribute]
        public string BookName { get; set; }
        [StringAttribute]
        public string? BookImg { get; set; }
        [StringAttribute]
        public string BookDetail { get; set; }
        [IntAttribute]
        public int? CateId { get; set; } 
        public CategoryResponse Cate { get; set; }
        [IntAttribute]
        public double? Price { get; set; }
        [IntAttribute]
        public int? CurrentQuantity { get; set; }
    }
}
