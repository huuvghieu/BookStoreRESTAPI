
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using Azure.Core;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service;
using BookStore.Service.DTO.Request;
using BookStore.Service.Exceptions;
using BookStore.Service.Service.InterfaceService;
using DataAcess.RequestModels;
using DataAcess.ResponseModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NTQ.Sdk.Core.BaseConnect;
using NTQ.Sdk.Core.CustomModel;
using NTQ.Sdk.Core.Utilities;
using NTQ.Sdk.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class OrderRepository :  IOrderRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseViewModel<OrderReponseModel>> CreateOrder(List<OrderCreateRequestModel> model, int userId)
        {
            try
            {
                if (model == null) throw new CrudException(HttpStatusCode.BadRequest, "Input Invalid", "");
                var user=_unitOfWork.Repository<User>().GetAll().FirstOrDefault(a=>a.UserId==userId);
                if(user== null) throw new CrudException(HttpStatusCode.NotFound, "User Not Found", "");
                OrderReponseModel response = new OrderReponseModel()
                {
                   OrderDate = DateTime.Now,
                   OrderReturnDate = DateTime.Now.AddMonths(2),
                   UserId = userId,
                   Status = 1
            };
                OrderBook o = _mapper.Map<OrderBook>(response);
                await _unitOfWork.Repository<OrderBook>().CreateAsync(o);
                await _unitOfWork.CommitAsync();
                foreach (var item in model)
                {
                    var book = _unitOfWork.Repository<Book>().GetAll().FirstOrDefault(x => x.BookId == item.BookId);
                    var m = _mapper.Map<OrderDetail>(item);
                    m.Price = (item.Quantity * (book.Price));
                    m.OrderId = o.OrderId;
                    await _unitOfWork.Repository<OrderDetail>().CreateAsync(m);
                    book.CurrentQuantity = book.CurrentQuantity - item.Quantity;
                    await _unitOfWork.Repository<Book>().Update(book, book.BookId);
                    await _unitOfWork.CommitAsync();
                }
                return new BaseResponseViewModel<OrderReponseModel>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = response
                };
            }

            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Create Order Error !", ex.InnerException?.Message);
            }
        }

        public async Task<BaseResponseViewModel<OrderDetailReponseModel>> DeleteItemOfOrder(int id)
        {
            var rs = _unitOfWork.Repository<OrderDetail>().GetAll().FirstOrDefault(a => a.OrderDetailId == id);
            if (rs == null) throw new CrudException(HttpStatusCode.NotFound, "Order Detail ID Is Not Found", "");
            try
            {
                 _unitOfWork.Repository<OrderDetail>().Delete(rs);
                var book = _unitOfWork.Repository<Book>().GetAll().FirstOrDefault(x => x.BookId == rs.BookId);
                book.CurrentQuantity = book.CurrentQuantity + rs.Quantity;
                await _unitOfWork.Repository<Book>().Update(book, book.BookId);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<OrderDetailReponseModel>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<OrderDetailReponseModel>(rs)
                };
            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Delete Order Detail Error !", ex.InnerException?.Message);
            }
        }

        public async Task<BaseResponseViewModel<OrderReponseModel>> GetOrder(int id)
        {
            try
            {
                var model = _unitOfWork.Repository<OrderBook>().GetAll().Where(a=>a.OrderId==id)
                    .Include(c => c.User).Include(c=>c.OrderDetails).Select(c => c.OrderDetails
                    .Select(rs => new OrderDetailReponseModel
                {
                    OrderId = rs.OrderId,
                    BookId = rs.BookId,
                    OrderDetailId = rs.OrderDetailId,
                    Price = rs.Price,
                    Quantity = rs.Quantity
                })).ToList();
                if (model == null) throw new CrudException(HttpStatusCode.NotFound, "Order Id Is Not Found", "");
                return new BaseResponseViewModel<OrderReponseModel>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<OrderReponseModel>(model)
                };
            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get Order By Id Error !", ex.InnerException?.Message);
            }
        }

        public async Task<BaseResponsePagingViewModel<OrderReponseModel>> GetOrders(PagingRequest? request, OrderRequestModel? model)
        {
            try
            {
                var filter = _mapper.Map<OrderReponseModel>(model);
                filter.SortDirection = request.SortDirection;
                filter.SortProperty = request.SortProperty;
                var response = _unitOfWork.Repository<OrderBook>().GetAll().Include(c => c.User).Select(c => c.OrderDetails.Select(rs => new OrderDetailReponseModel
                {
                    OrderId = rs.OrderId,
                    BookId = rs.BookId,
                    OrderDetailId = rs.OrderDetailId,
                    Price = rs.Price,
                    Quantity = rs.Quantity
                }));
                var rp= response.ProjectTo<OrderReponseModel>(_mapper.ConfigurationProvider).DynamicFilter(filter).DynamicSort(filter);
                if (request.PagingModel == null)
                {
                    var rs = rp.PagingQueryable(1, 10).Item2;
                    return new BaseResponsePagingViewModel<OrderReponseModel>()
                    {
                        Data = rs.ToList(),
                        Metadata = request.PagingModel
                    };
                }
                else
                {
                    var result = rp.PagingQueryable(request.PagingModel.Page, request.PagingModel.Size).Item2;
                    return new BaseResponsePagingViewModel<OrderReponseModel>()
                    {
                        Data = result.ToList(),
                        Metadata = request.PagingModel
                    };
                }
            }
            catch (Exception e)
            {

                throw new CrudException(HttpStatusCode.BadRequest, "Get Books Paging Error!!!", e.InnerException?.Message);
            }
        }

       public async Task<BaseResponseViewModel<OrderDetailReponseModel>>UpdateItemOfOrder(int id, OrderDetailUpdateRequestModel order)
        {
            var rs = _unitOfWork.Repository<OrderDetail>().GetAll().FirstOrDefault(a => a.OrderDetailId == id);
            if (rs == null) throw new CrudException(HttpStatusCode.NotFound, "", "");
            try
            {
                var book = _unitOfWork.Repository<Book>().GetAll().FirstOrDefault(x => x.BookId == rs.BookId);
                book.CurrentQuantity = book.CurrentQuantity - (order.Quantity - rs.Quantity);
                await _unitOfWork.Repository<Book>().Update(book, book.BookId);
                await _unitOfWork.CommitAsync();
                var updateOrder = _mapper.Map<OrderDetailUpdateRequestModel, OrderDetail>(order, rs);
                updateOrder.Price=book.Price*updateOrder.Quantity;
                await _unitOfWork.Repository<OrderDetail>().Update(updateOrder, id);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<OrderDetailReponseModel>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<OrderDetailReponseModel>(rs)
                };
            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Update Order Error !", ex.InnerException?.Message);
            }
        }
    }
}
