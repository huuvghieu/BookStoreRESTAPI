
using BookStore.Service.Helpers;
using Microsoft.Data.SqlClient;
using NTQ.Sdk.Core.Attributes;
using NTQ.Sdk.Core.CustomModel;
using NTQ.Sdk.Core.ViewModels;
using System.Text.Json.Serialization;
using static BookStore.Service.Helpers.SortType;

namespace BookStore.Service.DTO.Request
{
    public class PagingRequest : SortModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Total { get; set; } = 50;

    }
}
