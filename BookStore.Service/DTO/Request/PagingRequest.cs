using BookStore.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Request
{
    public class PagingRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortDirection { get; set; }
        public string? SortProperty { get; set; }
    }
}
