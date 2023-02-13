
using BookStore.Service.Helpers;
using Microsoft.Data.SqlClient;
using NTQ.Sdk.Core.CustomModel;
using NTQ.Sdk.Core.ViewModels;
using static BookStore.Service.Helpers.SortType;

namespace BookStore.Service.DTO.Request
{
    public class PagingRequest:SortModel
    {
        public PagingMetadata? PagingModel { get; set; }
        
    }
}