using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.InterfaceService
{
    public interface IOrderService
    {
        Task<BaseResponsePagingViewModel<OrderResponse>> GetOrders(PagingRequest pagingRequest);
        Task<BaseResponseViewModel<OrderResponse>> PostOrder(CategoryRequest model);
        Task<BaseResponseViewModel<OrderResponse>> PutOrder(int id, CategoryRequest model);
        Task<BaseResponseViewModel<OrderResponse>> DeleteOrder(int id);
        Task<BaseResponseViewModel<OrderResponse>> GetOrderByID(int id);
    }
}
