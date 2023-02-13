using AutoMapper;
using BookStore.Data.Models;
using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;

namespace BookStore.API.Mapper;


public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<CategoryResponse, Category>().ReverseMap();
        CreateMap<CategoryRequest, Category>();
        CreateMap<CategoryResponse, CategoryRequest>().ReverseMap();

        CreateMap<RegisterationRequest, User>();
        CreateMap<UserResponse, User>().ReverseMap();
        CreateMap<UserRequest, User>();

        CreateMap<OrderResponse, OrderBook>().ReverseMap();
        CreateMap<OrderDetailResponse, OrderDetail>().ReverseMap();
        CreateMap<OrderDetailRequest, OrderDetail>();

        CreateMap<BookResponse, Book>().ReverseMap();
    }
}
