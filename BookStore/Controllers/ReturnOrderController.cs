using BookStore.Service.DTO.Request;
using BookStore.Service.DTO.Response;
using BookStore.Service.Service.InterfaceService;
using DataAcess.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/return")]
    [ApiController]
    public class ReturnOrderController : ControllerBase
    {
        private readonly IReturnOrderService _returnService;
        public ReturnOrderController(IReturnOrderService returnService)
        {
            _returnService = returnService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderReponseModel>> ReturnOrder(ReturnOrderRequest returnOrderRequest, int userId)
        {
            var rs = await _returnService.ReturnOrder(returnOrderRequest, userId);
            return Ok(rs);
        }
    }
}
