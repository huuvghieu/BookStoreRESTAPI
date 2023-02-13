using NTQ.Sdk.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.DTO.Response
{
    public class CategoryResponse : SortModel
    {
        public int CateId { get; set; }
        public string? CateName { get; set; }
    }
}
