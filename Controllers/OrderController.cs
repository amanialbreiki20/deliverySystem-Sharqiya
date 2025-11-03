using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Dtos.Order;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace deliverySystem_Sharqiya.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IMessageHandler messageHandler)
            : base(messageHandler)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderAsync(List<CreateOrderInput> input)
        {
            _orderService.Header = BindRequestHeader();
            var response = await _orderService.CreateOrderAsync(input);
            return GetServiceResponse(response);
        }



        [HttpPost("get")]
        public async Task<IActionResult> GetOrdersAsync([FromBody] GlobalFilterDto input)
        {
            _orderService.Header = BindRequestHeader();
            var response = await _orderService.GetOrdersAsync(input);
            return GetServiceResponse(response);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] UpdateOrderInput input)
        {
            _orderService.Header = BindRequestHeader();
            var response = await _orderService.UpdateOrderAsync(id, input);
            return GetServiceResponse(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            _orderService.Header = BindRequestHeader();
            var response = await _orderService.DeleteOrderAsync(id);
            return GetServiceResponse(response);
        }
    }
}
