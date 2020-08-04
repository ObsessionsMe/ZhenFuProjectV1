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
        private IHostingEnvironment hostEnvironment;
        public CashManageController(ICashRepository _cashRepository, IHostingEnvironment _hostingEnvironment)
        {
            cashRepository = _cashRepository;
            hostEnvironment = _hostingEnvironment;
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
            bool bo = service.ApplyUserCash(cashType, ids);
            if (!bo)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "审批失败", data = null });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = null });
        }


        /// <summary>
        /// 导出订单列表
        /// </summary>
        /// <returns></returns>
        [Route("ExportCashExcel")]
        public ActionResult ExportCashExcel(PaginationParam param)
        {
            CashManageServices service = new CashManageServices(cashRepository);
            var pagination = param.pagination;
            string keyword = param.keyword;
            var orderList = service.GetCashList(pagination, keyword);
            Dictionary<string, string> cellheader = new Dictionary<string, string> {
                { "Name", "会员姓名" },
                { "userTelephone", "手机号" },
                { "GoodsName", "产品名称" },
                { "Type", "兑现类别" },
                { "Status", "兑现状态" },
                { "BankTypeName", "银行卡名称" },
                { "Account", "银行账号" },
                { "BankUserName", "开户人" },
                { "ProductPorints", "个人积分余额" },
                { "TreamPorints", "团队积分余额" },
                { "ProvinceName", "省份" },
                { "CityName", "城市" },
                { "ProvinceCityName", "开户行所在地" },
                { "Deduct", "兑现积分" },
                { "ActualDeduct", "转账金额(已扣除费率)" },
                { "DeductRate", "可兑现比例" },
                { "Date", "兑现提交时间" },
                { "Addtime", "会员注册时间" },
            };
            string urlPath = ExcelHelper.EntityListToExcel(cellheader, orderList, "兑现列表", hostEnvironment);
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = urlPath });
        }
    }
}
