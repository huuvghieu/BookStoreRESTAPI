using AutoMapper;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Exceptions;
using BookStore.Service.Helpers;
using BookStore.Service.Service.InterfaceService;
using DataAcess.ResponseModels;
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

        public async Task<BaseResponseViewModel<OrderReponseModel>> ReturnOrder(ReturnOrderRequest returnRequest, int userId)
        {
            try
            {
                if (returnRequest == null) throw new Exception();
                var user = _unitOfWork.Repository<User>().GetAll().FirstOrDefault(u => u.UserId == userId);
                if (user == null) throw new Exception();
                var orderBook = _unitOfWork.Repository<OrderBook>().GetAll().Include(u => u.OrderDetails)
                                                        .FirstOrDefault(u => u.OrderId == returnRequest.OrderID && u.UserId == userId);
                if (orderBook.Status == (int)StatusType.StatusOrder.Returned)
                {
                    throw new Exception();
                }
                var orderDetailList = orderBook.OrderDetails.ToList();
                var orderDetail = orderDetailList.FirstOrDefault(u => u.BookId == returnRequest.BookID);
                if (returnRequest.Quantity > orderDetail.Quantity) throw new Exception();
                int newQuantity = orderDetail.Quantity - returnRequest.Quantity;
                if (returnRequest.Quantity > orderDetail.Quantity) throw new Exception();
                var book = _unitOfWork.Repository<Book>().GetAll()
                                                            .FirstOrDefault(u => u.BookId == returnRequest.BookID);
                if (newQuantity == 0)
                {
                    orderDetail.Quantity = newQuantity;
                    book.CurrentQuantity += returnRequest.Quantity;
                    orderDetail.Price = newQuantity * book.Price;
                }
                if (newQuantity > 0)
                {
                    orderDetail.Quantity = newQuantity;
                    book.CurrentQuantity += returnRequest.Quantity;
                    orderDetail.Price = newQuantity * book.Price;
                }
                int check = 0;
                foreach (var item in orderDetailList)
                {
                    if (item.Quantity == 0)
                    {
                        check++;
                    }
                };
                if (check == orderDetailList.Count)
                {
                    orderBook.Status = (int)StatusType.StatusOrder.Returned;
                    orderBook.OrderReturnDate = DateTime.Now;
                }
                else
                {
                    orderBook.Status = (int)StatusType.StatusOrder.Borrowing;
                }
                var responseOrder = _mapper.Map<OrderReponseModel>(orderBook);
                var responesOrderDetail = _mapper.Map<OrderDetailReponseModel>(orderDetail);
                var responseBook = _mapper.Map<BookReponseModel>(orderDetail.Book);
                await _unitOfWork.Repository<OrderBook>().Update(orderBook, orderBook.OrderId);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.Repository<OrderDetail>().Update(orderDetail, orderDetail.OrderDetailId);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.Repository<Book>().Update(orderDetail.Book, orderDetail.Book.BookId);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<OrderReponseModel>()
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
                throw new CrudException(HttpStatusCode.BadRequest, "Return Order Error!!!", ex.InnerException?.Message);
            }
        }
    }
}
