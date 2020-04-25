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
    ///  用户收货地址，添加/删除/修改
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiveAddressController : BaseControllers
    {
        /// <summary>
        ///  获取用户默认收货地址
        /// </summary>
        /// <returns></returns>
        [Route("GetDefalutAddress")]
        public ActionResult GetUserDefalutAddress()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        ///  获取用户所有收货地址
        /// </summary>
        /// <returns></returns>
        [Route("GetUserAllAddress")]
        public ActionResult GetUserAllAddress()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }


        /// <summary>
        /// 用户添加/修改收货地址
        /// </summary>
        /// <returns></returns>
        [Route("SubmitAddress")]
        public ActionResult SubmitAddress()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 用户删除收货地址
        /// </summary>
        /// <returns></returns>
        [Route("RemoveAddress")]
        public ActionResult RemoveAddress()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

    }
}
