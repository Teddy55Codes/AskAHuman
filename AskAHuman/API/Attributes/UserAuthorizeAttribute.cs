using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AskAHuman.API.Attributes;

public class UserAuthorize : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity?.IsAuthenticated ?? false)
        {
            context.Result = new UnauthorizedResult(); 
            return;
        }
        
        var userId = user.FindFirst("userId");
        if (userId == null) context.Result = new UnauthorizedResult();
        
        // TODO: add role specific permission checks
    }
}