using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Dtos.Driver;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace deliverySystem_Sharqiya.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController(IDriverService driverService
        , IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        private readonly IDriverService _driverService = driverService;
        private readonly IMessageHandler _messageHandler = messageHandler;

        [HttpGet]
        public async Task<IActionResult> GetDriversAsync([FromQuery] GlobalFilterDto input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            _driverService.Header = BindRequestHeader();
            return GetServiceResponse(await _driverService.GetDriversAsync(input));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriverAsync([FromBody] List<CreateDriverInput> input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            _driverService.Header = BindRequestHeader();
            return GetServiceResponse(await _driverService.CreateDriverAsync(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriverAsync(int id, [FromBody] UpdateDriverInput input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            _driverService.Header = BindRequestHeader();
            return GetServiceResponse(await _driverService.UpdateDriverAsync(id, input));
        }
    }
}
