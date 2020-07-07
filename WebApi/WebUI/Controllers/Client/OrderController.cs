﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using BusinessLogic.ClientService;
using BusinessLogic.ManageService;
using Entity;
using Infrastructure;
using Infrastructure.LogConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.RepositoryService;
using Repository.ServiceInterface;
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
        private readonly IUserRepository userRepository;
        private readonly IUserBasePorintsRecordRepository basePorintRepository;
        private readonly IUserProductFrameworkRepository framkRepository;
        public OrderController(IGoodsRepository _goodsRepository, IReceiveAddressRepository _receiveAddressRepository, IOrderRepository _orderRepository,
            IUserPrintsSumRepository _sumRepository, IUserPorintsRecordRepository _recordRepository,
            IUserRepository _userRepository, IUserBasePorintsRecordRepository _basePorintRepository, IUserProductFrameworkRepository _framkRepository
            )
        {
            goodsRepository = _goodsRepository;
            receiveAddressRepository = _receiveAddressRepository;
            orderRepository = _orderRepository;
            sumRepository = _sumRepository;
            recordRepository = _recordRepository;
            userRepository = _userRepository;
            basePorintRepository = _basePorintRepository;
            framkRepository = _framkRepository;
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
            GoodsService servers = new GoodsService(goodsRepository, orderRepository);
            var goodsData = servers.GetGoodsDetails(goodsId);
            if (goodsData == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "商品详情数据为空", data = "" });
            }
            //获取收货地址
            ReceiveAddressService rserver = new ReceiveAddressService(receiveAddressRepository);
            var receiveAddressData = rserver.GetDefalutReveiveAddress(userModel.UserId);
            //获取该产品及用户对应的推荐人
            var userRefferEntity = framkRepository.FindEntity(x => x.UserId == userModel.UserId && x.GoodsId == goodsId);
            //if (receiveAddressData == null)
            //{
            //    //前端判断，如果数量为0，提醒用户添加收货地址
            //    return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户的收货地址数据为空", data = "" });
            //}
            var result = new
            {
                goodsData = goodsData,
                receiveAddressData = receiveAddressData,
                userRefferEntity = userRefferEntity
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = result });
        }

        /// <summary>
        /// 提交订单
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
            OrderService server = new OrderService(orderRepository, sumRepository, recordRepository, goodsRepository, userRepository, basePorintRepository, framkRepository);
            var data = server.SubmitOrder(orderInfo, userModel.UserId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "下单失败", data = "" });
            }
            if (data.state.ToString() == "error")
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = data.message, data = "" });
            }
            return Json(data);
        }

        [Route("CheckUserPayGoodsCount")]
        public ActionResult CheckUserPayGoodsCount(string goodsId, int payNum)
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败，请重新登录", data = "" });
            }
            OrderService server = new OrderService(orderRepository, sumRepository, recordRepository, goodsRepository, userRepository, basePorintRepository,framkRepository);
            var data = server.CheckUserPayGoodsCount(payNum, goodsId, userModel.UserId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = data.message, data = data });
            }
            return Json(data);
        }
    }
}
