using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.InterfaceService
{
    public interface IUserService
    {
        Task<BaseResponsePagingViewModel<UserResponse>> GetUsers(PagingRequest pagingRequest);
        Task<BaseResponseViewModel<UserResponse>> PutUser(int id, UserRequest model);
        Task<BaseResponseViewModel<UserResponse>> DeleteUser(int id);
        Task<BaseResponseViewModel<UserResponse>> GetUserByID(int id);
    }
}
