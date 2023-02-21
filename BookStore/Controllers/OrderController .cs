using AutoMapper;
using DataAcess.RequestModels;
using DataAcess.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTQ.Sdk.Core.CustomModel;
using NTQ.Sdk.Core.ViewModels;
using System.Data;
using System.Net;
using BookStore.Service.Service.InterfaceService;
using BookStore.Service.DTO.Request;

namespace OrderStore_API.Controllers
{
    [Route("api/OrderAPI")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
           _orderRepository = orderRepository;
        }
        [HttpGet("paging")]
        public async Task<ActionResult<List<OrderReponseModel>>> GetOrdersPaging([FromQuery]OrderRequestModel model, [FromQuery] PagingRequest request)
        {
            var rs = await _orderRepository.GetOrders(request, model);
            return Ok(rs);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderReponseModel>> GetOrderById(int id)
        {
            var rs = await _orderRepository.GetOrder(id);
            return Ok(rs);
        }
        [HttpPost()]
        public async Task<ActionResult<List<OrderReponseModel>>> CreateOrder([FromBody] OrderCreateRequestModel model)
        {
            var rs=await _orderRepository.CreateOrder(model);
            return Ok(rs);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderDetailReponseModel>> DeleteOrder(int id)
        {
            var rs = await _orderRepository.DeleteItemOfOrder(id);
            return Ok(rs);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderDetailReponseModel>>UpdateOrder(int id, [FromBody] OrderDetailUpdateRequestModel model)
        {
            var rs=await _orderRepository.UpdateItemOfOrder(id, model);
            return Ok(rs);
        }
    }
}
