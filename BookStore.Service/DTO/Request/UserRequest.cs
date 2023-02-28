using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Request
{
    public class UserRequest
    {
        public string? UserName { get; set; }

        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; } = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

        public string? Gender { get; set; }

        public string? Password { get; set; }


    }
}
