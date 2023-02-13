using AutoMapper;
using BookStore.Data.Models;
using BookStore.Data.UnitOfWork;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Exceptions;
using BookStore.Service.Service.InterfaceService;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NTQ.Sdk.Core.CustomModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Service.ImplService
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private string? _secretKey;
        private IMapper _mapper;
        public AuthUserService(IUnitOfWork unitOfWork,IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _secretKey = configuration.GetValue<string>("ApiSetting:Secret");
        }

        public bool IsUniqueUser(string Email)
        {
            var user = _unitOfWork.Repository<User>().Find(u => u.Email == Email);
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<BaseResponseViewModel<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Email == loginRequest.Email
                                                                     && u.Password == loginRequest.Password);
            if(user == null)
            {
                return new BaseResponseViewModel<LoginResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 1,
                        Message = "fail",
                        Success = false
                    },
                    Data = new LoginResponse
                    {
                        Token = "",
                        User = null
                    }
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new BaseResponseViewModel<LoginResponse>()
            {
                Status = new StatusViewModel
                {
                    ErrorCode = 0,
                    Message = "success",
                    Success = true
                },
                Data = new LoginResponse
                {
                    Token = tokenHandler.WriteToken(token),
                    User = user
                }
            };
        }

        public async Task<BaseResponseViewModel<UserResponse>> Registeration(RegisterationRequest registerationRequest)
        {
            try
            {
                var isUnique = IsUniqueUser(registerationRequest.Email);
                if (!isUnique)
                {
                    throw new Exception();
                }
                if(registerationRequest == null)
                {
                    throw new Exception();
                }
                var user = _mapper.Map<User>(registerationRequest);
                user.Role = "customer";
                await _unitOfWork.Repository<User>().CreateAsync(user);
                await _unitOfWork.CommitAsync();
                return new BaseResponseViewModel<UserResponse>()
                {
                    Status = new StatusViewModel
                    {
                        ErrorCode = 0,
                        Message = "success",
                        Success = true
                    },
                    Data = _mapper.Map<UserResponse>(user)
                };
            }
            catch(Exception ex)
            {
                throw new CrudException(HttpStatusCode.BadRequest, "Register Error!!!", ex.InnerException?.Message);
            }
        }

    }
}
