using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Entity;
using Entity.Params;
using Infrastructure;
using Infrastructure.LogConfig;
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
        private IUserPrintsSumRepository sumRepository;
        private IOrderRepository order;
        public UserController(IUserRepository _userRepository, IUserPrintsSumRepository _sumRepository, IOrderRepository _order)
        {
            userRepository = _userRepository;
            sumRepository = _sumRepository;
            order = _order;
        }

        [Route("getTeamEarn")]
        public ActionResult GetTeamEarn(GoodsParam param)
        {
            var result = new AjaxResult<dynamic>();
            try
            {
                param.UserId = userModel.UserId;
                //获取团队的兑现详情
                var ds = sumRepository.GetTeamEarn(param);
                var list = ds.Tables[0].ToDynamicList();
                var total = ds.Tables[1].ToDynamics().First();
                result.data = new
                {
                    list = list,
                    total = total
                };
                result.state = ResultType.success.ToString();
            }
            catch (Exception ex)
            {
                result.message = "获取个人收益失败!";
                result.state = ResultType.success.ToString();
                LogHelper.Log.Error(ex);
            }
            return Json(result);

        }

        [Route("getProductEarn")]
        public ActionResult GetProductEarn(GoodsParam param)
        {
            var result = new AjaxResult<dynamic>();
            try
            {
                param.UserId = userModel.UserId;
                //获取用户的兑现详情
                var ds = sumRepository.GetProductEarn(param);
                var list = ds.Tables[0].ToDynamicList();
                var total = ds.Tables[1].ToDynamics().First();
                result.data = new { 
                    list = list, 
                    total = total
                };
                result.state = ResultType.success.ToString();
            }
            catch (Exception ex)
            {
                result.message = "获取个人收益失败!";
                result.state = ResultType.success.ToString();
                LogHelper.Log.Error(ex);
            }
            return Json(result);

        }


        [Route("GetMyTream")]
        public ActionResult GetMyTream()
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败", data = "" });
            }
            //返回用户层级结构(包含自己总共三层)
            UserService servers = new UserService(userRepository, sumRepository,order);
            var data = userRepository.FindEntity(x => x.UserId == userModel.UserId && x.UserTelephone == userModel.UserTelephone && x.Enable == "Y");
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户不存在", data = "" });
            }
            var result = servers.GetMyTream(userModel.UserTelephone);
            var results = new
            {
                parentName = data.Referrer + data.ReferrerTelephone,
                name = data.Name,
                treeData = result,
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = results });
        }

        /// <summary>
        /// 获取用户积分(商品积分，团队积分)，进入个人首页时调用该方法
        /// </summary>
        /// <param name="userId"></param>
        [Route("GetUserPorints")]
        public ActionResult GetUserPorints(string userId)
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败", data = "" });
            }
            UserService servers = new UserService(userRepository, sumRepository,order);
            var data = servers.GetUserPorints(userId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "找不到用户相关的积分数据", data = "" });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = data });
        }
    }
}
