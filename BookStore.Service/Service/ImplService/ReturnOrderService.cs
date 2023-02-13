using AutoMapper;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Exceptions;
using BookStore.Service.Helper;
using BookStore.Service.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            try
            {
                var orderBook = _unitOfWork.Repository<OrderBook>().GetAll().Include(u => u.OrderDetails)
                                                        .FirstOrDefault(u => u.OrderId == returnRequest.OrderID && u.UserId == returnRequest.UserID);
                if (orderBook.Status == (int)StatusType.Status.None)
                {
                    throw new Exception();
                }
                var orderDetailList = orderBook.OrderDetails.ToList();
                var orderDetail = orderDetailList.FirstOrDefault(u => u.BookId == returnRequest.BookID);
                int newQuantity = orderDetail.Quantity - returnRequest.Quantity;
                var book = _unitOfWork.Repository<Book>().GetAll()
                                                            .FirstOrDefault(u => u.BookId == returnRequest.BookID);
                if (newQuantity == 0)
                {
                    orderDetail.Quantity = newQuantity;
                    book.CurrentQuantity += returnRequest.Quantity;
                    orderBook.Status = (int)StatusType.Status.None;
                    orderBook.OrderReturnDate = DateTime.Now;
                    orderDetail.Price = newQuantity * book.Price;
                }
                if (newQuantity >= 0)
                {
                    orderDetail.Quantity = newQuantity;
                    book.CurrentQuantity += returnRequest.Quantity;
                    orderBook.Status = (int)StatusType.Status.Borrowing;
                    orderDetail.Price = newQuantity * book.Price;
                }
                var responseOrder = _mapper.Map<OrderResponse>(orderBook);
                var responesOrderDetail = _mapper.Map<OrderDetailResponse>(orderDetail);
                var responseBook = _mapper.Map<BookResponse>(orderDetail.Book);
                await _unitOfWork.Repository<OrderBook>().Update(orderBook, orderBook.OrderId);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.Repository<OrderDetail>().Update(orderDetail, orderDetail.OrderDetailId);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.Repository<Book>().Update(orderDetail.Book, orderDetail.Book.BookId);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<OrderResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "success",
                        Success = true,
                    },
                    Data = responseOrder
                };
            }
            catch(Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get Books Paging Error!!!", e.InnerException?.Message);
            }
        }
    }
}
