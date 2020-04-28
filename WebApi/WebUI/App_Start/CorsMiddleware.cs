using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Tool;

namespace WebUI.App_Start
{
    /// <summary>
    ///  设置域的访问权限-解决跨域问题
    /// </summary>
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;
        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Referer"))
            {
                string Url = context.Request.Headers["Referer"];
                string[] arr = Url.Split('/');
                if (Url.Substring(Url.Length - 1, 1) == "/")
                {
                    Url = Url.Substring(0, Url.Length - 1);//如果最后一个字符为/,就移除掉
                }
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                //允许所有来源访问      
                context.Response.Headers.Add("Access-Control-Allow-Origin", Url);
                //允许访问的方式
                context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                //用于判断request来自ajax还是传统请求
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin,X-Requested-With,Content-Type,Accept,Token");
                context.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            }
            await _next(context);
        }
    }
}
