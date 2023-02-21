using NTQ.Sdk.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NTQ.Sdk.Core.Attributes;

namespace BookStore.Service.DTO.Response
{
    public class CategoryResponse : SortModel
    {
        [IntAttribute]
        public int? CateId { get; set; } = null;
        [StringAttribute]
        public string? CateName { get; set; }


    }
}
