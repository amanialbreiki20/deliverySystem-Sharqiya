using deliverySystem_Sharqiya.Helpers;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Helpers.ServiceContext;
using deliverySystem_Sharqiya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Security.Claims;

namespace deliverySystem_Sharqiya.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController(IMessageHandler messageHandler) : ControllerBase
    {
        private readonly IMessageHandler _messageHandler = messageHandler;

        [NonAction]
        public RequestHeaderContent BindRequestHeader()
        {

            var language = HttpContext?.Request?.Headers["Accept-Language"].FirstOrDefault() ??
                           HttpContext?.Request?.Headers["lang"].FirstOrDefault() ??
                           HttpContext?.Request?.Query["language"].ToString() ??
                           HttpContext?.Request?.Query["lang"].ToString() ??
                           "en_US";

            var endpoint = HttpContext?.GetEndpoint();
            var endpointName = string.Empty;

            if (endpoint != null)
            {
                var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();

                if (controllerActionDescriptor != null)
                {
                    var controllerName = controllerActionDescriptor.ControllerTypeInfo.FullName;
                    var actionName = controllerActionDescriptor.ActionName;

                    endpointName = $"{controllerName}.{actionName}";
                }
            }

            var x = HttpContext.User;
            var userType = UserType.User;

            if (HttpContext.User.FindFirstValue(Constants.UserType) == "Admin")
            {
                userType = UserType.Admin;
            }

            //if (HttpContext.User.FindFirstValue(Constants.UserType) == "SuperAdmin")
            //{
            //    userType = UserType.SuperAdmin;
            //}


            var userContext = new RequestHeaderContent
            {
                UserId = int.Parse(HttpContext.User.FindFirstValue(Constants.UserId)),
                UserType = userType,
                UserMobileNumber = HttpContext.User.FindFirstValue(Constants.UserMobileNumber),
                UserEmail = HttpContext.User.FindFirstValue(Constants.UserEmail),
                EndPointName = endpointName,
            };

            return userContext;
        }

        [NonAction]
        public ActionResult GetServiceResponse<T>(ServiceResponse<T> response)
        {
            if (!response.Succeed)
            {
                return BadRequest(new ApiResponse(response.Code, response.Description, response.Result));
            }

            return Ok(new ApiResponse(response.Code, response.Description, response.Result));
        }

        [NonAction]
        public ActionResult GetServiceResponse<T, R>(ServiceResponse<T, R> response)
        {
            if (!response.Succeed)
            {
                return BadRequest(new ApiResponse(response.Code, response.Description));
            }

            return Ok(new ApiResponse(response.Code, response.Description, response.Result, response.Metadata));
        }

        [NonAction]
        public ActionResult GetServiceResponse(ServiceResponse response)
        {
            var apiResponse = new ApiResponse(response.Code, response.Description);
            return response.Succeed ? Ok(apiResponse) : BadRequest(apiResponse);
        }

        [NonAction]
        public BadRequestObjectResult InvaidInput()
        {
            return BadRequest(new ApiResponse((int)ErrorMessage.BadRequest, _messageHandler.GetMessage(ErrorMessage.BadRequest), ErrorExtractor.GetErrors(ModelState)));
        }
    }
}
