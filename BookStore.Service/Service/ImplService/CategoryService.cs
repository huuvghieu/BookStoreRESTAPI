using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Exceptions;
using BookStore.Service.Helpers;
using BookStore.Service.Service.InterfaceService;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using NTQ.Sdk.Core.CustomModel;
using NTQ.Sdk.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.ImplService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponseViewModel<CategoryResponse>> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetAsync(u => u.CateId == id);
            try
            {
                if(id <= 0)
                {
                    throw new Exception();
                }
                 _unitOfWork.Repository<Category>().Delete(category);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<CategoryResponse>()
                {
                    Status = new StatusViewModel
                    {
                        Success = true,
                        ErrorCode = 0,
                        Message = "sucess"
                    },
                    Data = null
                };
            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Delete Category Error!!!", ex.InnerException?.Message);
            }
        }

        public async Task<BaseResponsePagingViewModel<CategoryResponse>> GetCategories(PagingRequest pagingRequest)
        {
            try
            {
                var filter = new CategoryResponse();
                filter.SortDirection = pagingRequest.SortDirection;
                filter.SortProperty = pagingRequest.SortProperty;
                filter.CateName = pagingRequest.KeySearch;

                var rsFilter = _unitOfWork.Repository<Category>().GetAll()
                                .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
                                .DynamicSort(filter).DynamicFilter(filter);

                var res = rsFilter.PagingQueryable(pagingRequest.Page, pagingRequest.Size);

                return new BaseResponsePagingViewModel<CategoryResponse>()
                {
                    Metadata = new PagingMetadata
                    {
                        Page = pagingRequest.Page,
                        Size = pagingRequest.Size,
                        Total = res.Item1
                    },
                    Data = res.Item2.ToList()
                };

            }
            catch(Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get Categories Paging Error!!!", ex.InnerException?.Message);
            }

        }

        public async Task<BaseResponseViewModel<CategoryResponse>> GetCategoryByID(int id)
        {
            try
            {
                if (id <= 0 )
                {
                    throw new Exception();
                }
                var response = await _unitOfWork.Repository<Category>().GetAsync(u => u.CateId == id);
                return new BaseResponseViewModel<CategoryResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "sucess",
                        Success = true
                    },
                    Data = _mapper.Map<CategoryResponse>(response)
                };

            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get Category By ID Error!!!", ex.InnerException?.Message);
            }

        }

        public async Task<BaseResponseViewModel<CategoryResponse>> PostCategory(CategoryRequest model)
        {
            var cateRequest = await _unitOfWork.Repository<Category>().GetAsync(u => u.CateName == model.CateName);
            try
            {
                if (cateRequest != null)
                {
                    throw new Exception();
                }
                var response = _mapper.Map<CategoryRequest ,Category>(model);
                await _unitOfWork.Repository<Category>().CreateAsync(response);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<CategoryResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "sucess",
                        Success = true
                    },
                    Data = _mapper.Map<CategoryResponse>(response)
                };
            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Create Category Error!!!", ex.InnerException?.Message);
            }
        }

        public async Task<BaseResponseViewModel<CategoryResponse>> PutCategory(int id, CategoryRequest model)
        {
            var category = await _unitOfWork.Repository<Category>().GetAsync(u => u.CateId == id);
            try
            {
                if(category == null)
                {
                    throw new Exception();
                }
                category.CateName = model.CateName;
                await _unitOfWork.Repository<Category>().Update(category, id);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<CategoryResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "sucess",
                        Success = true
                    },
                    Data = _mapper.Map<CategoryResponse>(category)
                };
            }
            catch(Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Update Category Error!!!", ex.InnerException?.Message);
            }
        }

    }
}
