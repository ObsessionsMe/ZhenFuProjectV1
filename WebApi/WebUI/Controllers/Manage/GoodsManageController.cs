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
    /// 商品管理模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsManageController : Controller
    {
        // GET: api/UserManage/GetGoodsList
        /// <summary>
        ///  获取所有商品信息
        /// </summary>
        /// <returns></returns>
        [Route("GetGoodsList")]
        public ActionResult GetGoodsList()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 管理员添加/修改收货地址
        /// </summary>
        /// <returns></returns>
        [Route("SubmitGoods")]
        public ActionResult SubmitGoods()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 用户删除收货地址
        /// </summary>
        /// <returns></returns>
        [Route("RemoveGoods")]
        public ActionResult RemoveGoods()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }
    }
}
