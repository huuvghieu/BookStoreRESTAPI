using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using DataAcess.ResponseModels;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.InterfaceService
{
    public interface IReturnOrderService
    {
        Task<NTQ.Sdk.Core.CustomModel.BaseResponseViewModel<OrderReponseModel>> ReturnOrder(ReturnOrderRequest returnRequest, int userId);
    }
}
