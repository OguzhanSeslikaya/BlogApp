using Blog.Application.Abstractions.Configurations;
using Blog.Application.Abstractions.Services;
using Blog.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace Blog.Api.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = context.HttpContext.User.Identity.Name;
            bool isAdmin = name != null ? await _userService.isAdmin(name):false;
            if (!string.IsNullOrEmpty(name) && !isAdmin)
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                if(attribute == null)
                {
                    await next();
                    return;
                }
                var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;
                
                var code = $"{attribute.menu}{(httpAttribute!=null? httpAttribute.HttpMethods.First() : HttpMethods.Get)}{attribute.actionType}{attribute.definition}".Replace(" ", "");

                bool hasPermission = await _userService.hasPermissionToAction(name,code);

                if (!hasPermission)
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                    await next();
            }else
                await next();
        }
    }
}
