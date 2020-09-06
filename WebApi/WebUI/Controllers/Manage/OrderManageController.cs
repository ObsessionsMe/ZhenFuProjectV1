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
using Repository.RepositoryService;
using RepositoryFactory.ServiceInterface;
using WebUI.Tool;

namespace WebUI.Controllers.Manage
{
    /// <summary>
    /// 商品管理模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManageController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private IHostingEnvironment hostEnvironment;
        private IReceiveAddressRepository receiveAddress;
        public OrderManageController(IOrderRepository _orderRepository, IHostingEnvironment _hostEnvironment, IReceiveAddressRepository _receiveAddress)
        {
            orderRepository = _orderRepository;
            hostEnvironment = _hostEnvironment;
            receiveAddress = _receiveAddress;
    }
        // GET: api/OrderManage/GetOrderList
        /// <summary>
        ///  获取所有商品信息
        /// </summary>
        /// <returns></returns>
        [Route("GetOrderList")]
        public ActionResult GetOrderList(PaginationParam param)
        {
            OrderManageServices service = new OrderManageServices(orderRepository);
            var pagination = param.pagination;
            string keyword = param.keyword;
            var data = new
            {
                rows = service.GetOrderList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Json(new AjaxResult { state = ResultType.success, message = "获取数据成功", data = data });
        }

        /// <summary>
        /// 设为已发货
        /// </summary>
        /// <returns></returns>
        [Route("SetOutGoods")]
        public ActionResult SetOutGoods(string orderNumber)
        {
            var entity = orderRepository.FindEntity(x => x.OrderNumber == orderNumber);
            var result = new AjaxResult
            {
                state = ResultType.error,
                message = "操作失败",
                data = null
            };
            if (entity == null)
            {
                return Json(result);
            }
            entity.OrderStatus = 2;
            int i = orderRepository.Update(entity);
            if (i <= 0)
            {
                return Json(result);
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = null });
        }

        /// <summary>
        /// 修改订单备注信息
        /// </summary>
        /// <returns></returns>
        [Route("EditOrderRemark")]
        public ActionResult EditOrderRemark(string orderNumber, string orderRemark)
        {
            var entity = orderRepository.FindEntity(x => x.OrderNumber == orderNumber);
            var result = new AjaxResult
            {
                state = ResultType.error,
                message = "操作失败",
                data = null
            };
            if (entity == null)
            {
                return Json(result);
            }
            entity.OrderRemark = orderRemark;
            int i = orderRepository.Update(entity);
            if (i <= 0)
            {
                return Json(result);
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = null });
        }

        /// <summary>
        /// 修改订单收货地址
        /// </summary>
        /// <returns></returns>
        [Route("EditOrderAddress")]
        public ActionResult EditOrderAddress(string orderNumber, string orderAddress)
        {
            var entity = orderRepository.FindEntity(x => x.OrderNumber == orderNumber);
            var result = new AjaxResult
            {
                state = ResultType.error,
                message = "操作失败",
                data = null
            };
            if (entity == null)
            {
                return Json(result);
            }
            var rec = receiveAddress.FindEntity(x => x.AddressId == entity.AddressId);
            rec.DetailsAddress = orderAddress;
            int i  = receiveAddress.Update(rec);
            if (i <= 0)
            {
                return Json(result);
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = null });
        }

        /// <summary>
        /// 导出订单列表
        /// </summary>
        /// <returns></returns>
        [Route("ExportOrderExcel")]
        public ActionResult ExportOrderExcel(PaginationParam param)
        {
            OrderManageServices service = new OrderManageServices(orderRepository);
            var pagination = param.pagination;
            string keyword = param.keyword;
            var orderList = service.GetOrderList(pagination, keyword);
            Dictionary<string, string> cellheader = new Dictionary<string, string> {
                { "OrderNumber", "订单编号" },
                { "Name", "会员姓名" },
                { "UserTelephone", "手机号" },
                { "GoodsName", "商品名称" },
                { "GoodsUnitPrice", "商品单价" },
                { "BuyGoodsNums", "数量" },
                { "PayCount", "订单总额" },
                { "PayMethod", "支付方式" },
                { "UsePorintsType", "积分类型" },
                { "ReceiveUser", "收货人姓名" },
                { "ReceiveTelephone", "收货人电话" },
                { "ReceiveProvinceName", "收货省" },
                { "ReceiveCityName", "收货市" },
                { "ReceiveAreaName", "收货区" },
                { "DetailsAddress", "详细地址" },
                { "orderStatus", "状态" },
                { "AddTime", "下单时间" }
            };
            string urlPath = ExcelHelper.EntityListToExcel(cellheader, orderList, "订单列表", hostEnvironment);
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = urlPath });
        }
    }
}
