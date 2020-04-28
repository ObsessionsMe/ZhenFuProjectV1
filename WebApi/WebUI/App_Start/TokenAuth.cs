
using Entity;
using Infrastructure;
using Infrastructure.LogConfig;
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
            object token = null;
            if (Request.Headers.Keys.Contains("Token"))
            {
                token = Request.Headers["Token"];
            }
            else if (Request.Headers.Keys.Contains("token"))
            {
                token = Request.Headers["token"];
                if (token == null || new CacheHelper().GetCache<UserInfoEntity>(token.ToString()) == null)
                {
                    token = null;
                }
            }
            if (token == null)
            {
                await context.HttpContext.Response.WriteAsync(new AjaxResult { state = ResultType.error.ToString(), message = "Token无效，请重新登录", data = "" }.ToJson());
                return;
            }
            await next();
        }
    }
}
