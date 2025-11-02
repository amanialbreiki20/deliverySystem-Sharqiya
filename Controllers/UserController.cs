using CRM.Dtos.Common;
using deliverySystem_Sharqiya.Dtos.User;
using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Helpers.ServiceContext;
using deliverySystem_Sharqiya.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace deliverySystem_Sharqiya.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IServiceExecution _serviceExecution;

        public UserController(
            IUserService userService,
            IMessageHandler messageHandler,
            IServiceExecution serviceExecution
        ) : base(messageHandler)
        {
            _userService = userService;
            _serviceExecution = serviceExecution;
        }

        
        [HttpPost("create")]
        public async Task<IActionResult> CreateUsersAsync([FromBody] List<CreateUserInput> input)
        {
            _userService.Header = BindRequestHeader();
            var response = await _userService.CreateUserAsync(input);
            return GetServiceResponse(response);
        }

        
        [HttpPost("list")]
        public async Task<IActionResult> GetUsersAsync([FromBody] GlobalFilterDto input)
        {
            _userService.Header = BindRequestHeader();
            var response = await _userService.GetUsersAsync(input);
            return GetServiceResponse(response);
        }

        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserInput input)
        {
            _userService.Header = BindRequestHeader();
            var response = await _userService.UpdateUserAsync(id, input);
            return GetServiceResponse(response);
        }

        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            _userService.Header = BindRequestHeader();
            var response = await _userService.DeleteUserAsync(id);
            return GetServiceResponse(response);
        }
    }
}
