﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Exceptions;
using BookStore.Service.Service.InterfaceService;
using NTQ.Sdk.Core.CustomModel;
using NTQ.Sdk.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.ImplService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponseViewModel<UserResponse>> DeleteUser(int id)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(u => u.UserId == id);
            try
            {
                if (id <= 0)
                {
                    throw new Exception();
                }
                _unitOfWork.Repository<User>().Delete(user);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<UserResponse>()
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
                throw new CrudException(HttpStatusCode.BadRequest, "Delete User Error!!!", ex.InnerException?.Message);
            }
        }

        public async Task<BaseResponseViewModel<UserResponse>> GetUserByID(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception();
                }
                var response = await _unitOfWork.Repository<User>().GetAsync(u => u.UserId == id);
                return new BaseResponseViewModel<UserResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "sucess",
                        Success = true
                    },
                    Data = _mapper.Map<UserResponse>(response)
                };

            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get User By ID Error!!!", ex.InnerException?.Message);
            }
        }

        public async Task<BaseResponsePagingViewModel<UserResponse>> GetUsers(PagingRequest pagingRequest)
        {
            try
            {
                var filter = new UserResponse();
                filter.SortDirection = pagingRequest.SortDirection;
                filter.SortProperty = pagingRequest.SortProperty;

                var rsFilter = _unitOfWork.Repository<User>().GetAll()
                                .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
                                .DynamicSort(filter).DynamicFilter(filter);


                var res = rsFilter.PagingQueryable(pagingRequest.Page, pagingRequest.PageSize);

                return new BaseResponsePagingViewModel<UserResponse>()
                {
                    Metadata = new PagingMetadata
                    {
                        Page = pagingRequest.Page,
                        Size = pagingRequest.PageSize,
                        Total = res.Item1
                    },
                    Data = res.Item2.ToList()
                };
            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Get Users Paging Error!!!", ex.InnerException?.Message);
            }

        }

        public async Task<BaseResponseViewModel<UserResponse>> PutUser(int id, UserRequest model)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(u => u.UserId == id);
            try
            {
                if (user == null)
                {
                    throw new Exception();
                }
                 var response = _mapper.Map<UserRequest, User>(model, user);
                await _unitOfWork.Repository<User>().Update(response, id);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<UserResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "sucess",
                        Success = true
                    },
                    Data = _mapper.Map<UserResponse>(user)
                };
            }
            catch (Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Update User Error!!!", ex.InnerException?.Message);
            }
        }
    }
}