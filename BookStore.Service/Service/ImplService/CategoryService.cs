using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using BookStore.Data.Extensions;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Exceptions;
using BookStore.Service.Helpers;
using BookStore.Service.Service.InterfaceService;
using DataAcess.ResponseModels;
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
using BookStore.Data.Extensions;

namespace BookStore.Service.Service.ImplService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<BaseResponseViewModel<CategoryResponse>> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetAsync(u => u.CateId == id);
            try
            {
                if (id <= 0)
                {
                    throw new Exception();
                }
                _unitOfWork.Repository<Category>().Delete(category);
                await _unitOfWork.CommitAsync();
                return new NTQ.Sdk.Core.CustomModel.BaseResponseViewModel<CategoryResponse>()
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

        public async Task<BasePagingViewModel<CategoryResponse>> GetCategories(PagingRequest pagingRequest, CategoryRequest categoryRequest)
        {
            try
            {
                var filter = _mapper.Map<CategoryResponse>(categoryRequest);

                filter.SortDirection = pagingRequest.SortDirection;
                filter.SortProperty = pagingRequest.SortProperty;

                var rsFilter = _unitOfWork.Repository<Category>().GetAll()
                                .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
                                .DynamicSort(filter).DynamicFilter(filter)
                                .PagingQueryable(pagingRequest.Page, pagingRequest.PageSize).Item2;
                return new BasePagingViewModel<CategoryResponse>()
                {
                    Metadata = pagingRequest,
                    Data = rsFilter.ToList()
                };

            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get Categories Paging Error!!!", ex.InnerException?.Message);
            }

        }

        public async Task<BaseResponseViewModel<CategoryResponse>> GetCategoryByID(int id)
        {
            try
            {
                var cacheData = _cacheService.GetCacheValue<CategoryResponse>($"Category{id}");
                if (cacheData == null)
                {
                    if (id <= 0)
                    {
                        throw new CrudException(HttpStatusCode.BadRequest, "Id Invalid", "");
                    }
                    var response = await _unitOfWork.Repository<Category>().GetAsync(u => u.CateId == id);
                    cacheData = _mapper.Map<CategoryResponse>(response);
                    _cacheService.SetCacheValue<CategoryResponse>($"Category{id}", cacheData);

                    return new BaseResponseViewModel<CategoryResponse>()
                    {
                        Status = new StatusViewModel
                        {
                            ErrorCode = 0,
                            Message = "sucess",
                            Success = true
                        },
                        Data = cacheData
                    };
                }
                return new BaseResponseViewModel<CategoryResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "sucess",
                        Success = true
                    },
                    Data = cacheData
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
                var response = _mapper.Map<CategoryRequest, Category>(model);
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
                if (category == null)
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
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Update Category Error!!!", ex.InnerException?.Message);
            }
        }

    }
}
