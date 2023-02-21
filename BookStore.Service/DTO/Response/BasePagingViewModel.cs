using BookStore.Service.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Response
{
    public class BasePagingViewModel<T>
    {
        public PagingRequest Metadata { get; set; }

        public List<T> Data { get; set; }
    }
}
