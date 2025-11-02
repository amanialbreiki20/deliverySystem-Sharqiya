using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

public class UserTypeFilter(string requiredUserType, string secondRequiredUserType) : Attribute, IAuthorizationFilter
{
    private readonly string _requiredUserType = requiredUserType;
    private readonly string _secondRequiredUserType = secondRequiredUserType;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (user == null || !user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userTypeClaim = user.Claims.FirstOrDefault(c => c.Type == "userType");
        if (userTypeClaim == null || ( userTypeClaim.Value != _requiredUserType && userTypeClaim.Value != _secondRequiredUserType))
        {
            context.Result = new ForbidResult();
        }
    }
}
