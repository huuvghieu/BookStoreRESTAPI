using AutoMapper;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Exceptions;
using BookStore.Service.Service.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.ImplService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<BaseResponseViewModel<OrderResponse>> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseViewModel<OrderResponse>> GetOrderByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponsePagingViewModel<OrderResponse>> GetOrders(PagingRequest pagingRequest)
        {
            var filter = new OrderResponse();
            filter.SortDirection = pagingRequest.SortDirection;
            filter.SortProperty = pagingRequest.SortProperty;
            throw new NotImplementedException();

        }

        public async Task<BaseResponseViewModel<OrderResponse>> PostOrder(OrderRequest orderRequest, OrderDetailRequest orderDetailRequest)
        {
            
            try
            {
                if (orderRequest == null || orderDetailRequest == null)
                {
                    throw new Exception();
                }
                var responseDetail = _mapper.Map<OrderDetail>(orderDetailRequest);
                orderRequest.OrderDetails.Add(responseDetail);
                var responseOrder = _mapper.Map<OrderBook>(orderRequest);
                await _unitOfWork.Repository<OrderBook>().CreateAsync(responseOrder);
                await _unitOfWork.Repository<OrderDetail>().CreateAsync(responseDetail);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<OrderResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "sucess",
                        Success = true
                    },
                    Data = _mapper.Map<OrderResponse>(responseOrder)
                };
            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Create Order Error!!!", ex.InnerException?.Message);
            }
        }

        public Task<BaseResponseViewModel<OrderResponse>> PutOrder(int id, CategoryRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
