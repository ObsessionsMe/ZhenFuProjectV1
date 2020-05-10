using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryFactory.ServiceInterface;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefalutController : Controller
    {
        private readonly ILogger<DefalutController> _logger;
        private readonly IUserRepository userRepository;
        private readonly IUserPrintsSumRepository sumRepository;
        public DefalutController(ILogger<DefalutController> logger, IUserRepository _userRepository, IUserPrintsSumRepository _sumRepository)
        {
            _logger = logger;
            userRepository = _userRepository;
            sumRepository = _sumRepository;
        }

        [HttpGet]
        public string Get()
        { 
            return "默认路由请求测试。。。";
        }

        [Route("GetUserInfo")]
        public ActionResult GetUserInfo()
        {
            //UserService servers = new UserService(userRepository, sumRepository, order);
            //var data = servers.FindUserList();
            //if (data == null)
            //{
            //    return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "未获取到用户数据" });
            //}
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "获取数据成功" });
        }
    }
}