using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Manage
{
    /// <summary>
    /// 用户管理模块(用户列表，用户提现申请列表)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserManageController : Controller
    {
        // GET: api/UserManage/LoginOn
        [Route("LoginOn")]
        public ActionResult LoginOn()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        [Route("LoginOut")]
        public ActionResult LoginOut()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 获取用户列表(会员)
        /// </summary>
        /// <returns></returns>
        [Route("GetUserList")]
        public ActionResult GetUserList()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        ///  获取用户消息通知(包括:1提现申请通知(数量)，2会员角色流转通知(数量))
        /// </summary>
        /// <returns></returns>
        [Route("GetAllNoticeMessage")]
        public ActionResult GetAllNoticeMessage()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 确认已完成转账(针对--用户提现)
        /// </summary>
        /// <returns></returns>
        [Route("GetAllNoticeMessage")]
        public ActionResult ConfirmTransfer()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 导出用户提现申请列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportUserCashListExcel()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

    }
}
