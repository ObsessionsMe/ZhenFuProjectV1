using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Entity;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryFactory.ServiceInterface;
using UtilitieEntity;
using WebUI.App_Start;
using WebUI.Tool;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  用户模块:登录,注册等...
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseControllers
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        /// <summary>
        /// 获取用户积分(商品积分，团队积分)，进入个人首页时调用该方法
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult GetUserPorints(string userTelephone, string referrer, string referrerTelephone)
        {
            if (string.IsNullOrEmpty(userTelephone) || string.IsNullOrEmpty(referrer) || string.IsNullOrEmpty(referrerTelephone))
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户或推荐人不存在", data = "" });
            }
            UserService servers = new UserService(userRepository);
            UserBaiseModel data = servers.GetUserPorints(userTelephone, referrer, referrerTelephone);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户不存在", data = "" });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = data });
        }

        [Route("GetMyTream")]
        public ActionResult GetMyTream()
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败", data = "" });
            }
            string userTelephone = "1";
            string userId = "1";
            //返回层级结构(包含自己总共三层)
            if (string.IsNullOrEmpty(userTelephone) || string.IsNullOrEmpty(userId))
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户不存在", data = "" });
            }
            UserService servers = new UserService(userRepository);
            var data = servers.GetMyTream(userTelephone, userId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户不存在", data = "" });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = data });
        }
    }
}
