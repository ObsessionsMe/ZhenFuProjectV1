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
                if (!Request.Headers.Keys.Contains("token"))
                {
                    return null;
                }
                var token = Request.Headers["token"];
                return new CacheHelper().GetCache<UserInfoEntity>(token);
            }
        }
    }
}
