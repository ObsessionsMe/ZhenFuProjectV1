using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.RepositoryService;
using RepositoryFactory.ServiceInterface;
using WebUI.App_Start;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  订单模块：购买商品提交订单/微信，支付宝支付
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseControllers
    {
        private readonly IGoodsRepository goodsRepository;
        private readonly IReceiveAddressRepository receiveAddressRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserPrintsSumRepository sumRepository;
        private readonly IUserPorintsRecordRepository recordRepository;
        public OrderController(IGoodsRepository _goodsRepository, IReceiveAddressRepository _receiveAddressRepository, IOrderRepository _orderRepository,
            IUserPrintsSumRepository _sumRepository, IUserPorintsRecordRepository _recordRepository
            )
        {
            goodsRepository = _goodsRepository;
            receiveAddressRepository = _receiveAddressRepository;
            orderRepository = _orderRepository;
            sumRepository = _sumRepository;
            recordRepository = _recordRepository;
        }
        /// <summary>
        ///  准备下单-获选获取收货地址和商品详情
        /// </summary>
        /// <returns></returns>
        [Route("ReadyPlaceOrder")]
        public ActionResult ReadyPlaceOrder(string goodsId)
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败，请重新登录", data = "" });
            }
            //获取商品详细
            GoodsService servers = new GoodsService(goodsRepository);
            var goodsData = servers.GetGoodsDetails(goodsId);
            if (goodsData == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "商品详情数据为空", data = "" });
            }
            //获取收货地址
            ReceiveAddressService rserver = new ReceiveAddressService(receiveAddressRepository);
            var receiveAddressData = rserver.GetUserReveiveAddressList(userModel.UserId);
            //if (receiveAddressData == null)
            //{
            //    //前端判断，如果数量为0，提醒用户添加收货地址
            //    return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户的收货地址数据为空", data = "" });
            //}
            var result = new
            {
                goodsData = goodsData,
                receiveAddressData = receiveAddressData
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = result });
        }

        /// <summary>
        /// 提交订单，跳过支付(暂时)，默认算作支付成功
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
         [Route("SubmitOrder")]
        public ActionResult SubmitOrder(string jsonString)
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败，请重新登录", data = "" });
            }
            //是否校验用户
            if (string.IsNullOrEmpty(jsonString))
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "订单传入的参数为空", data = "" });
            }
            OrderInfoEntity orderInfo = JsonConvert.DeserializeObject<OrderInfoEntity>(jsonString);
            OrderService server = new OrderService(orderRepository, sumRepository, recordRepository, goodsRepository);
            var data = server.SubmitOrder(orderInfo, userModel.UserId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "下单失败", data = data });
            }
            if (data.state.ToString() == "error")
            {
                return Json(data);
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = data });
        }
    }
}
