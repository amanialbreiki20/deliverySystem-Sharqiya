using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using deliverySystem_Sharqiya.Helpers.MessageHandler;
using deliverySystem_Sharqiya.Helpers;

namespace deliverySystem_Sharqiya.Security.ACL
{
    internal class ClaimRequirementFilter : IAsyncAuthorizationFilter
    {
        private readonly Claim _claim;
        private readonly IMessageHandler _messageHandler;

        public ClaimRequirementFilter(Claim claim, IMessageHandler messageHandler)
        {
            _claim = claim;
            _messageHandler = messageHandler;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            if (!hasClaim)
            {
                // Return a custom error response
                context.Result = new ForbidResultError(new ApiResponse((int)ErrorMessage.Forbidden, _messageHandler.GetMessage(ErrorMessage.Forbidden)));

            }

            return Task.CompletedTask;
        }

        public class ForbidResultError : ObjectResult
        {
            public ForbidResultError(object value) : base(value)
            {
                StatusCode = StatusCodes.Status403Forbidden;
            }
        }
    }

}