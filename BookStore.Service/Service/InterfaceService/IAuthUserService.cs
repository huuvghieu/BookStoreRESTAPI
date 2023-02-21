using BookStore.Data.Models;
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
    public interface IAuthUserService
    {
        bool IsUniqueUser(string Email);
        Task<NTQ.Sdk.Core.CustomModel.BaseResponseViewModel<LoginResponse>> Login(LoginRequest loginRequest);
        Task<NTQ.Sdk.Core.CustomModel.BaseResponseViewModel<UserResponse>> Registeration(RegisterationRequest registerationRequest); 
    }
}
