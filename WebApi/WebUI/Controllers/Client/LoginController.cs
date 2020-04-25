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
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
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
    public class LoginController : Controller
    {
        private readonly IUserRepository userRepository;
        public static IHttpContextAccessor Context;
        public LoginController(IUserRepository _userRepository, IHttpContextAccessor _Context)
        {
            userRepository = _userRepository;
            Context = _Context;
        }
        // GET: api/User/LoginOn
        [Route("LoginOn")]
        public ActionResult LoginOn(string telephone, string password)
        {
            //校验用户Telephone合法性
            UserService servers = new UserService(userRepository);
            var data = servers.GetUserNameByPhone(telephone);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "该手机号在系统中不存在", data = "" });
            }
            //密码解密
            string password_r = DESEncrypt.Decrypt(data.Password);
            if (telephone != data.UserTelephone || password != password_r)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "手机号或密码错误", data = "" });
            }
            data = servers.CheckLogin(telephone, data.Password);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "手机号或密码错误", data = "" });
            }
            string key = $"userInfoCache{Guid.NewGuid().ToString("N")}";
            //CacheHelper._cache.Set<UserInfoEntity>(key, data);
            UserInfoEntity datas = data;
            new CacheHelper().SetCache(key, datas);
            var result = new
            {
                token = key,
                data = data
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = result });
        }

        [Route("LoginOut")]
        public ActionResult LoginOut()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 忘记密码，将会进入到重置密码
        /// </summary>
        /// <returns></returns>
        [Route("EditPassWord")]
        public ActionResult EditPassWord(string telephone, string newPassword, string newPassword_r)
        {
            if (newPassword != newPassword_r)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "两次密码输入不一致", data = "" });
            }
            UserService servers = new UserService(userRepository);
            //校验手机号
            if (!string.IsNullOrEmpty(telephone))
            {
                var data = servers.GetUserNameByPhone(telephone);
                if (data == null)
                {
                    return Json(new AjaxResult { state = ResultType.error.ToString(), message = "该手机号在系统中不存在", data = "" });
                }
            }
            var user = servers.EditPassWord(telephone, newPassword);
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = user.UserId });
        }

        /// <summary>
        ///  用户注册
        /// </summary>
        /// <returns></returns>
        [Route("UserRegister")]
        public ActionResult UserRegister(string jsonString)
        {
            UserInfoEntity userInfo = JsonConvert.DeserializeObject<UserInfoEntity>(jsonString);
            UserService servers = new UserService(userRepository);
            if (userInfo == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "获取数据失败", data = "" });
            }
            AjaxResult rtnMessage = servers.UserRegister(userInfo);
            if (rtnMessage.state.ToString() == "error")
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = rtnMessage.message, data = "" });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = userInfo});
        }

        /// <summary>
        ///  获取短信验证码
        /// </summary>
        /// <returns></returns>
        [Route("GetPhoneCode")]
        public ActionResult GetPhoneCode(string mobile)
        {
            //if (string.IsNullOrEmpty(mobile))
            //{
            //    return Json(new AjaxResult { state = ResultType.error.ToString(), message = "手机验号不能为空", data = "" });
            //}
            //int NoteCode = new NoteCode().SendNote(mobile);//发送短信
            //if (NoteCode == -1)
            //{
            //    return Json(new AjaxResult { state = ResultType.error.ToString(), message = "获取手机验证码失败", data = "" });
            //}
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        ///  通过用户手机号获取对应姓名
        /// </summary>
        /// <returns></returns>
        [Route("GetUserNameByPhone")]
        public ActionResult GetUserNameByPhone(string telephone)
        {
            UserService servers = new UserService(userRepository);
            var data = servers.GetUserNameByPhone(telephone);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "该手机号在系统中不存在", data = "" });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = data.Name });
        }
    }
}
