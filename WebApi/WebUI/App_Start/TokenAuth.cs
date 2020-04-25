
using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Tool;

namespace WebUI.App_Start
{
    public class TokenAuthAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var Request = context.HttpContext.Request;
            var error = new
            {
                state = "error",
                message = "Token无效，请重新登录"
            };
            if (!Request.Headers.Keys.Contains("token"))
            {
                await context.HttpContext.Response.WriteAsync("Token无效，请重新登录");
            }
            var token = Request.Headers["token"];
            //var user = CacheHelper._cache.Get<UserInfoEntity>(token);
            var user = new CacheHelper().GetCache<UserInfoEntity>(token);
            if (user != null)
            {
                await next();
            }
            else
            {
                await context.HttpContext.Response.WriteAsync("Token无效，请重新登录");
            }
        }
    }
}
