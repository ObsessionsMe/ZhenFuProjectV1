using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        private IUserProductFrameworkRepository framekRepository;
        public UserController(IUserRepository _userRepository, IUserPrintsSumRepository _sumRepository, IOrderRepository _order, IUserProductFrameworkRepository _framekRepository)
        {
            userRepository = _userRepository;
            sumRepository = _sumRepository;
            order = _order;
            framekRepository = _framekRepository;
        }

        [Route("getTeamEarnDetail")]
        public ActionResult GetTeamEarnDetail(GoodsParam param)
        {
            var result = new AjaxResult<dynamic>();
            try
            {
                param.UserId = userModel.UserId;
                //获取团队的兑现详情
                var ds = sumRepository.GetTeamEarnDetail(param);
                var list = ds.Tables[0].ToDynamicList();
                var detail = ds.Tables[1].ToDynamics().First();
                result.data = new
                {
                    list = list,
                    detail = detail
                };
                result.state = ResultType.success.ToString();
            }
            catch (Exception ex)
            {
                result.message = "获取团队收益详情!";
                result.state = ResultType.success.ToString();
                LogHelper.Log.Error(ex);
            }
            return Json(result);
        }

        [Route("getTeamDetail")]
        public string GetTeamDetail(GoodsParam param)
        {
            UserService servers = new UserService(userRepository, sumRepository, order);
            var result = new AjaxResult<dynamic>();
            try
            {
                param.UserId = userModel.UserId;
                //获取团队的详情
                var ds = sumRepository.GetTeamDetail(param);
                var treeTable = ds.Tables[1];
                var root = treeTable.AsEnumerable().Where(x => x.Field<int>("Level") == -1).Select(x => new UserTreeData(x)).FirstOrDefault();
                root.label = root.name + "(" + root.telephone.Substring(root.telephone.Length - 5, 4) + "):" + root.buyGoodsCount;
                servers.FillTreeData(treeTable, root);
                var tree = new List<UserTreeData>();
                tree.Add(root);

                var detail = ds.Tables[0].ToDynamics().First();
                result.data = new
                {
                    tree = tree,
                    detail = detail
                };
                result.state = ResultType.success.ToString();
            }
            catch (Exception ex)
            {
                result.message = "获取团队详情!";
                result.state = ResultType.success.ToString();
                LogHelper.Log.Error(ex);
            }
            var jsonStr = JsonConvert.SerializeObject(result);
            return jsonStr;
        }

        [Route("getTeamOrderList")]
        public ActionResult GetTeamOrderList(GoodsParam param)
        {
            var result = new AjaxResult<dynamic>();
            try
            {
                param.UserId = userModel.UserId;
                //获取团队的产品订单详情
                var ds = sumRepository.GetTeamOrderList(param);
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
                result.message = "获取团队订单详情失败!";
                result.state = ResultType.success.ToString();
                LogHelper.Log.Error(ex);
            }
            return Json(result);
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
                result.message = "获取团队收益失败!";
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


        [Route("GetMyTream")]
        public dynamic GetMyTream()
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败", data = "" });
            }
            //返回用户层级结构(包含自己总共三层)
            var data = userRepository.FindEntity(x => x.UserId == userModel.UserId && x.UserTelephone == userModel.UserTelephone && x.Enable == "Y");
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户不存在", data = "" });
            }
            UserService servers = new UserService(userRepository, sumRepository, order);
            var result = servers.GetMyTream_New(userModel.UserId);
            DataTable treeTable = userRepository.GetUserTeamLevel(userModel.UserId);
            int payNum = Convert.ToInt32(treeTable.Compute("sum(BuyAllGoodsNums)", ""));
            var results = new
            {
                parentName = data.Referrer + data.ReferrerTelephone,
                name = data.Name,
                treeData = result,
                payNum = payNum
            };
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = results });
            //return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = results });
            return jsonStr;
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
            UserService servers = new UserService(userRepository, sumRepository, order);
            var user = servers.GetUserPorints_Base(userId);
            if (user == null)
            {
                user = new UserInfoEntity();
                user.PecialItemPorints = 0;
                user.PorintsSurplus = 0;
                user.TreamPorints = 0;
                user.TourismPorints = 0;
                user.FieldsPorints = 0;
            }
            var result = new
            {
                pecialItemPorints = user.PecialItemPorints,
                porintsSurplus = user.PorintsSurplus,
                TreamPorints = user.TreamPorints,
                TourismPorints = user.TourismPorints,
                FieldsPorints = user.FieldsPorints
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = result });
        }

        [Route("GetPorintSurplus")]
        public ActionResult GetPorintSurplus(GoodsParam param)
        {
            param.UserId = userModel.UserId;
            var result = new VipProductDetailModel();
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败", data = "" });
            }
            var entity = sumRepository.FindEntity(x => x.GoodsId == param.GoodsId && x.UserId == userModel.UserId);
            if (entity == null)
            {
                result.ProductPorints = 0;
                result.TreamPorints = 0;
                result.HoldingDays = 0;
                result.IsAgency = false;
            }
            else
            {
                result.ProductPorints = entity.ProductPorints;
                result.TreamPorints = entity.TreamPorints;
                result.HoldingDays = entity.HoldingDays;
                result.IsAgency = sumRepository.IsAgency(param);
            }

            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = result });
        }
    }
}
