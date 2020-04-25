using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WebUI.App_Start;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  积分模块：积分兑现/积分充值
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserPorintsController : BaseControllers
    {
        /// <summary>
        ///  获取用户剩余积分,
        /// </summary>
        /// <returns></returns>
        [Route("GetUserSurplusPoints")]
        public ActionResult GetUserSurplusPoints()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 用户充值积分
        /// </summary>
        /// <returns></returns>
        [Route("SubmitPaycheck")]
        public ActionResult SubmitPaycheck()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        ///  获取用户所有积分，包括1当前积分数额，2可提现积分数额
        /// </summary>
        /// <returns></returns>
        [Route("GetUserAllPoints")]
        public ActionResult GetUserAllPoints()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 发起提现的申请
        /// </summary>
        /// <returns></returns>
        [Route("SubmitExchangeInfo")]
        public ActionResult SubmitExchangeInfo()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

    }
}
