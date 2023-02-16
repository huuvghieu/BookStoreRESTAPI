using BookStore.Service.DTO.Request;
using DataAcess.RequestModels;
using DataAcess.ResponseModels;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.InterfaceService
{
    public interface IOrderRepository
    {
        Task<BaseResponseViewModel<OrderReponseModel>> CreateOrder(List<OrderCreateRequestModel> model,int userId);
        Task<BaseResponseViewModel<OrderReponseModel>> GetOrder(int id);
        Task<BaseResponsePagingViewModel<OrderReponseModel>> GetOrders(PagingRequest request, OrderRequestModel model);
        Task<BaseResponseViewModel<OrderDetailReponseModel>>UpdateItemOfOrder(int id, OrderDetailUpdateRequestModel order);
        Task<BaseResponseViewModel<OrderDetailReponseModel>> DeleteItemOfOrder(int id);

    }
}
