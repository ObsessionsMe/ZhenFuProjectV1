using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ManageService;
using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryFactory.ServiceInterface;
using WebUI.Tool;

namespace WebUI.Controllers.Manage
{
    /// <summary>
    /// 商品管理模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsManageController : Controller
    {
        private readonly IGoodsRepository goodsRepository;
        public GoodsManageController(IGoodsRepository _goodsRepository)
        {
            goodsRepository = _goodsRepository;
        }
        // GET: api/GoodsManage/GetGoodsList
        /// <summary>
        ///  获取所有商品信息
        /// </summary>
        /// <returns></returns>
        [Route("GetGoodsList")]
        public ActionResult GetGoodsList(PaginationParam param)
        {
            GoodsManageServices service = new GoodsManageServices(goodsRepository);
            var pagination = param.pagination;
            string keyword = param.keyword;
            var data = new
            {
                rows = service.GetGoodsList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Json(new AjaxResult { state = ResultType.success, message = "获取数据成功", data = data });
        }

        /// <summary>
        /// 提交商品数据(新增/保存)
        /// </summary>
        /// <returns></returns>
        [Route("SubmitGoods")]
        public ActionResult SubmitGoods()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }
    }
}
