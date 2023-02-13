
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service;
using BookStore.Service.DTO.Request;
using BookStore.Service.Exceptions;
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
    public class BookRepository :  IBookRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseViewModel<BookReponseModel>> CreateBook(BookRequestModel model)
        {
            try
            {
                if (model == null) throw new CrudException(HttpStatusCode.BadRequest, "Input Invalid","");
                var rs = _unitOfWork.Repository<Book>().GetAll().FirstOrDefault(a => a.BookName.ToLower() == model.BookName.ToLower());
                if (rs != null ) throw new CrudException(HttpStatusCode.BadRequest, "Book already exists!","");
                var creatBook=_mapper.Map<BookRequestModel,Book>(model);
                await _unitOfWork.Repository<Book>().CreateAsync(creatBook);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<BookReponseModel>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<BookReponseModel>(creatBook)
                };
            }
          
                catch (Exception ex)
                {
                    throw new CrudException(HttpStatusCode.BadRequest, "Create Book Error !", ex.InnerException?.Message);
                }
            }

            public async Task<BaseResponseViewModel<BookReponseModel>> DeleteBook(int id)
        {
            var rs = _unitOfWork.Repository<Book>().GetAll().FirstOrDefault(a => a.BookId == id);
            if (rs == null) throw new CrudException(HttpStatusCode.NotFound, "", ""); 
                try
                {
                    _unitOfWork.Repository<Book>().Delete(rs);
                    await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<BookReponseModel>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<BookReponseModel>(rs)
                };
            }
                catch (Exception ex)
                {
                    throw new CrudException(HttpStatusCode.BadRequest, "Delete Book Error !", ex.InnerException?.Message);
                }
        }
        public async Task<BaseResponseViewModel<List<BookReponseModel>>> GetBestSellers()
        {
            try
            {
                var model = _unitOfWork.Repository<OrderDetail>().GetAll().GroupBy(a => a.BookId).Select(x => new
                {
                    BookID = x.Key,
                    sell_number = x.Count()
                });
                var order = model.FirstOrDefault(x => x.sell_number == model.Select(x => x.sell_number).Max());
                var result= _unitOfWork.Repository<Book>().GetAll().Include(c => c.Cate).OrderBy(c => c.BookId).Where(c => c.BookId == order.BookID).ToList();
                return new BaseResponseViewModel<List<BookReponseModel>>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<List<BookReponseModel>>(result)
                };
            }
            catch(Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get Books Best Seller Error !", ex.InnerException?.Message);
            }
            
        }

        public async Task<BaseResponseViewModel<BookReponseModel>> GetBook(int id)
        {
                try
                {
                    var model = _unitOfWork.Repository<Book>().GetAll().Include(c => c.Cate).FirstOrDefault(c => c.BookId == id);
                if (model == null) throw new CrudException(HttpStatusCode.NotFound, "", "");
                return new BaseResponseViewModel<BookReponseModel>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<BookReponseModel>(model)
                };
            }
                catch (Exception ex)
                {
                    throw new CrudException(HttpStatusCode.BadRequest, "Get Book By Id Error !", ex.InnerException?.Message);
                } 
        }

        public  async Task<BaseResponseViewModel<List<BookReponseModel>>> GetBookByCateById(int cateId)
        {
                try
                {
                    var model = _unitOfWork.Repository<Book>().GetAll().Include(c => c.Cate).Where(c => c.CateId == cateId).ToList();
                if (model == null) throw new CrudException(HttpStatusCode.NotFound, "", "");
                return new BaseResponseViewModel<List<BookReponseModel>>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<List<BookReponseModel>>(model)
                };
            }
                catch (Exception ex)
                {
                    throw new CrudException(HttpStatusCode.BadRequest, "Get Book By Category Id Error !", ex.InnerException?.Message);
                }
        }
        public async Task<BaseResponsePagingViewModel<BookReponseModel>> GetBooks(PagingRequest request,BookRequestModel model)
        {
                try
                {
                var filter=_mapper.Map<BookReponseModel>(model);
                filter.SortDirection = request.SortDirection;
                filter.SortProperty=request.SortProperty;
                var response = _unitOfWork.Repository<Book>().GetAll().Include(a=>a.Cate).ProjectTo<BookReponseModel>(_mapper.ConfigurationProvider).DynamicFilter(filter).DynamicSort(filter);
                if (request.PagingModel == null)
                {
                    var rs= response.PagingQueryable(1,10).Item2;
                    return new BaseResponsePagingViewModel<BookReponseModel>()
                    {
                        Data = rs.ToList(),
                        Metadata = request.PagingModel
                    };
                }
                else
                {
                    var result = response.PagingQueryable(request.PagingModel.Page, request.PagingModel.Size).Item2;
                    return new BaseResponsePagingViewModel<BookReponseModel>()
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
        public async Task<BaseResponseViewModel<BookReponseModel>> UpdateBook(int id, BookRequestModel model)
        {
            var book =  _unitOfWork.Repository<Book>().GetAll().Include(a=>a.Cate).FirstOrDefault(a => a.BookId == id);
            if (book == null) throw new CrudException(HttpStatusCode.NotFound, "", "");
                try
                {
                    var updateBook = _mapper.Map<BookRequestModel,Book>(model,book);
                    await _unitOfWork.Repository<Book>().Update(updateBook,id);
                    await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<BookReponseModel>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Sucess",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<BookReponseModel>(book)
                };
            }
                catch(Exception ex)
                {
                    throw new CrudException(HttpStatusCode.BadRequest, "Update Book Error !", ex.InnerException?.Message);
                }
        }
    }
}
