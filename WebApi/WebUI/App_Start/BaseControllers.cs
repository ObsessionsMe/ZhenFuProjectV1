using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Tool;

namespace WebUI.App_Start
{
    [TokenAuth]
    public class BaseControllers : Controller
    {
        public UserInfoEntity userModel
        {
            get
            {
                object token = null;
                if (Request.Headers.Keys.Contains("Token"))
                {
                    token = Request.Headers["Token"];
                }
                else if (Request.Headers.Keys.Contains("token"))
                {
                    token = Request.Headers["token"];
                }
                if (token == null)
                {
                    return null;
                }
                return new CacheHelper().GetCache<UserInfoEntity>(token.ToString());
            }
        }
    }
}
