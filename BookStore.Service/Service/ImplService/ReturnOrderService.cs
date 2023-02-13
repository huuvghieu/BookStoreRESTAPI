using AutoMapper;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Helper;
using BookStore.Service.Service.InterfaceService;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.ImplService
{
    public class ReturnOrderService : IReturnOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseViewModel<OrderResponse>> ReturnOrder(ReturnOrderRequest returnRequest)
        {
            var orderRequest = await _unitOfWork.Repository<OrderBook>().GetAsync(u => u.OrderId == returnRequest.OrderID 
                                                                            && u.UserId == returnRequest.UserID);
            var oderBook = _mapper.Map<OrderBook>(orderRequest);
            foreach (var item in order.OrderDetails)
            {
                if(item.BookId == returnRequest.BookID)
                {
                    int newQuantity = item.Quantity - returnRequest.Quantity;
                    if( newQuantity == 0)
                    {
                        item.Quantity = newQuantity;
                        item.Book.CurrentQuantity = item.Book.CurrentQuantity + returnRequest.Quantity;
                        order.OrderReturnDate = order.OrderReturnDate;
                        order.Status = (int)StatusType.Status.None;
                    }
                    if(newQuantity >=0)
                    {
                        item.Quantity = newQuantity;
                        item.Book.CurrentQuantity = item.Book.CurrentQuantity + returnRequest.Quantity;
                        order.Status = (int)StatusType.Status.Borrowing;
                    }
                }
            }
            var response = _mapper.Map<OrderResponse>(order);
            await _unitOfWork.Repository<OrderBook>().Update(order, order.OrderId);
            await _unitOfWork.CommitAsync();
            return new BaseResponseViewModel<OrderResponse>()
            {
                Status = new StatusViewModel
                {
                    ErrorCode = 0,
                    Message = "success",
                    Success = true,
                },
                Data = response
            };
        }
    }
}
