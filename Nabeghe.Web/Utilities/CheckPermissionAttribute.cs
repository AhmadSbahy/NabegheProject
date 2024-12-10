
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;

namespace Nabeghe.Web.Utilities
{
    public class CheckPermissionAttribute:AuthorizeAttribute,IAsyncAuthorizationFilter
    {
        private readonly string permissionName;

        public CheckPermissionAttribute(string permissionName)
        {
            this.permissionName = permissionName;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userService = context.HttpContext.RequestServices
                .GetRequiredService<IUserService>();
            int currentUserId = context.HttpContext.User.GetUserId();
            if(!userService.UserHasPermission(currentUserId,permissionName))
            {
                context.Result = new RedirectResult("/Login");
            }
            return Task.CompletedTask;
        }
    }
}
