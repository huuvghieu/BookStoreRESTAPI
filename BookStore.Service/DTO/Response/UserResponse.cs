using NTQ.Sdk.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Response
{
    public class UserResponse : SortModel
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
