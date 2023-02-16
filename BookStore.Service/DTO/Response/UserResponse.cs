using BookStore.Data.Models;
using NTQ.Sdk.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTQ.Sdk.Core.Attributes;

namespace BookStore.Service.DTO.Response
{
    public class UserResponse : SortModel
    {
        public int UserId { get; set; }
        [String]
        public string? UserName { get; set; }
        [String]
        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; }
        [String]
        public string? Gender { get; set; }
        public string? Password { get; set; }
        [String]
        public string? Email { get; set; }
        public virtual ICollection<OrderBook> OrderBooks { get; } = new List<OrderBook>();
    }
}
