using deliverySystem_Sharqiya.Dtos.Auth;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Services.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace deliverySystem_Sharqiya.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [Consumes("application/json", "multipart/form-data")]

    public class AuthController(IAuthService authService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {

        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInput input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            return GetServiceResponse(await _authService.LoginAsync(input));
        }


        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordInput input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            return GetServiceResponse(await _authService.ResetPasswordAsync(input));
        }

        [HttpPost("setNewPassword")]
        public async Task<IActionResult> SetNewPasswordAsync([FromBody] SetNewPasswordInput input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            return GetServiceResponse(await _authService.SetNewPasswordAsync(input));
        }
    }
}
