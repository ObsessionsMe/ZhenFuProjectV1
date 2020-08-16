using Aop.Api.Domain;
using Entity;
using Infrastructure;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Repository.RepositoryService;
using Repository.ServiceInterface;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtilitieEntity;

namespace BusinessLogic.ClientService
{
    public class OrderService
    {
        /// <summary>
        ///  订单模块业务逻辑
        /// </summary>
        private IOrderRepository orderRepository;
        private IUserPrintsSumRepository sumRepository;
        private IUserPorintsRecordRepository recordRepository;
        private IGoodsRepository goodsRepository;
        private IUserRepository userRepository;
        private IUserBasePorintsRecordRepository basePorintRepository;
        private IUserProductFrameworkRepository frameWork;
        private IProductCfgRepository productCfgRepository;
        public OrderService(IOrderRepository _orderRepository, IUserPrintsSumRepository _sumRepository,
            IUserPorintsRecordRepository _recordRepository, IGoodsRepository _goodsRepository,IUserRepository _userRepository,
            IUserBasePorintsRecordRepository _basePorintRepository, IUserProductFrameworkRepository _frameWork, IProductCfgRepository _productCfgRepository)
        {
            orderRepository = _orderRepository;
            sumRepository = _sumRepository;
            recordRepository = _recordRepository;
            goodsRepository = _goodsRepository;
            userRepository = _userRepository;
            basePorintRepository = _basePorintRepository;
            frameWork = _frameWork;
            productCfgRepository = _productCfgRepository;
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        public AjaxResult SubmitOrder(OrderInfoEntity order, string userId)
        {
            try
            {
                //1:扣除该用户的积分，类型为余额积分
                var userEntity = userRepository.FindEntity(x => x.UserId == userId && x.Enable == "Y");
                if (userEntity == null)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你账号无效，请使用其他账号登录", data = "" };
                }
                //校验当前用户的推荐人都买了哪一级的产品，如果买了E，他就可以买ABCDE产品，如果买了B，就可以买AB产品
                //var goodsEntity = goodsRepository.FindEntity(x => x.GoodsId  == order.GoodsId&& x.Enable == "Y");
                //if (goodsEntity.isProduct == "Y")
                //{
                //    if (userEntity.UserId != "20200505001")
                //    {
                //        bool bo = orderRepository.IsOverStp_PayMaxGoodsLeve(userId, order.GoodsId);
                //        if (!bo)
                //        {
                //            return new AjaxResult { state = ResultType.error.ToString(), message = "下单失败！你当前购买的产品不能大于你推荐人购买的产品等级，", data = "" };
                //        }
                //    }
                //}
                int i = 0;
                int payCount = (order.GoodsUnitPrice) * (order.BuyGoodsNums);  //下单总价
                //减去运费
                if (order.GoodsFreight > 0)
                {
                    payCount = payCount - order.GoodsFreight;
                }
                if (order.UsePorintsType == 1)
                {
                    //福豆余额结算
                    int PorintsSurplus = userEntity.PorintsSurplus;
                    if (PorintsSurplus <= 0)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的福豆余额不足，请先充值", data = "" };
                    }
                    if (PorintsSurplus < payCount)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的福豆余额不足，请先充值", data = "" };
                    }
                    userEntity.PorintsSurplus = (userEntity.PorintsSurplus) - (payCount);

                    //赠送专项积分
                    var prodCfgEntity = productCfgRepository.FindEntity(x => x.GoodsId == order.GoodsId && x.isGivePorint == "Y");
                    if (prodCfgEntity != null)
                    {
                        userEntity.TourismPorints += prodCfgEntity.isGiveDefalutPorint * order.BuyGoodsNums;
                    }
                }
                if (order.UsePorintsType == 3)
                {
                    //可用福豆结算
                    int PecialItemPorints = userEntity.PecialItemPorints;
                    if (PecialItemPorints <= 0)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的可用福豆不足", data = "" };
                    }
                    if (PecialItemPorints < payCount)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的可用福豆不足", data = "" };
                    }
                    userEntity.PecialItemPorints = (userEntity.PecialItemPorints) - (payCount);
                }
                i = userRepository.Update(userEntity);//扣除积分
                if (i < 1)
                {
                    return null;
                }
                //2:订单表入库
                var orderEntity = new OrderInfoEntity();
                orderEntity.OrderNumber = "SP" + Common.CreateNo() + Common.RndNum(5);
                if (order.BuyGoodsNums <= 0 || order.GoodsUnitPrice <= 0)
                {
                    return null;
                }
                orderEntity.GoodsUnitPrice = order.GoodsUnitPrice;
                orderEntity.BuyGoodsNums = order.BuyGoodsNums;
                orderEntity.GoodsId = order.GoodsId;
                orderEntity.UserId = userId;
                orderEntity.OrderStatus = 1;
                orderEntity.PayCount = payCount;
                orderEntity.UsePorintsType = order.UsePorintsType;
                orderEntity.AddressId = order.AddressId;
                orderEntity.OrderRemark = order.OrderRemark;
                orderEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                i = orderRepository.Insert(orderEntity);
                if (i < 1)
                {
                    return null;
                }
                //SUM表初始化
                var sumEntity = sumRepository.FindEntity(x => x.UserId == userEntity.UserId && x.GoodsId == order.GoodsId);
                if (sumEntity == null)
                {
                    var sumporintsEntity = new UserPrintsSumEntity();
                    sumporintsEntity.UserId = userEntity.UserId;
                    sumporintsEntity.GoodsId = order.GoodsId;
                    sumporintsEntity.ProductPorints = 0;
                    sumporintsEntity.TreamPorints = 0;
                    sumporintsEntity.HoldingDays = 0;
                    sumporintsEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    sumRepository.Insert(sumporintsEntity);
                    if (i < 1)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "下单失败", data = "" };
                    }
                }
                var result = new
                {
                    goodsId = order.GoodsId
                };
                return new AjaxResult { state = ResultType.success.ToString(), message = "下单成功！", data = result };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AjaxResult CheckUserPayGoodsCount(int payNum, string goodsId, string userId)
        {
            var orderList = orderRepository.FindList(x => x.UserId == userId && x.GoodsId == goodsId);
            int alreadPayCount = 0;
            foreach (var item in orderList)
            {
                alreadPayCount += Convert.ToInt32(item.BuyGoodsNums);
            }
            if (alreadPayCount == 0)
            {
                if (payNum > 20)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你当前的购买数量已经超过了最大数额(20件)，请选择其他产品进行购买", data = null };
                }
            }
            int count = Convert.ToInt32(20 - alreadPayCount);
            if (payNum > count)
            {
                return new AjaxResult { state = ResultType.error.ToString(), message = "你当前的购买数量已经超过了最大数额(20件)，请选择其他产品进行购买", data = null };
            }
            return new AjaxResult { state = ResultType.success.ToString(), message = "操作成功！", data = count };
        }
    }
}
