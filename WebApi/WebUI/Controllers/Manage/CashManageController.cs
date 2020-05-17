using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ManageService;
using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryFactory.ServiceInterface;
using WebUI.Tool;

namespace WebUI.Controllers.Manage
{
    /// <summary>
    /// 商品管理模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CashManageController : Controller
    {
        private readonly ICashRepository cashRepository;
        public CashManageController(ICashRepository _cashRepository)
        {
            cashRepository = _cashRepository;
        }
        [Route("GetCashList")]
        public ActionResult GetCashList(PaginationParam param)
        {
            CashManageServices service = new CashManageServices(cashRepository);
            var pagination = param.pagination;
            string keyword = param.keyword;
            var data = new
            {
                rows = service.GetCashList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Json(new AjaxResult { state = ResultType.success, message = "获取数据成功", data = data });
        }
        [Route("UserCashApply")]
        public ActionResult UserCashApply(int cashType, string ids)
        {
            CashManageServices service = new CashManageServices(cashRepository);
            bool bo = service.UserCashApply(cashType, ids);
            if (!bo)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "审批失败", data = null });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = null });
        }
    }
}
