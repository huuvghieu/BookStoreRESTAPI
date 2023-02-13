using AutoMapper;
using BookStore.Data.Models;
using DataAcess.RequestModels;
using DataAcess.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessTier.Mapper
{
    public class Mapping : Profile
    {
        public Mapping() {
            CreateMap<BookRequestModel, BookReponseModel>().ReverseMap();
            CreateMap<BookRequestModel, Book>().ReverseMap();
            CreateMap<BookReponseModel, Book>().ReverseMap();

            CreateMap<CategoryReponseModel, Category>().ReverseMap();

            CreateMap<OrderDetailRequestModel, OrderDetailReponseModel>().ReverseMap();
            CreateMap<OrderCreateRequestModel, OrderDetailReponseModel>().ReverseMap();
            CreateMap<OrderDetailRequestModel, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailReponseModel, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailReponseModel, OrderReponseModel>().ReverseMap();
            CreateMap<OrderCreateRequestModel, OrderDetail>().ReverseMap();
            CreateMap<OrderRequestModel, OrderReponseModel>().ReverseMap();
            CreateMap<OrderReponseModel, OrderBook>().ReverseMap();
            CreateMap<OrderReponseModel, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailUpdateRequestModel, OrderDetail>().ReverseMap();
        }

    }
}
