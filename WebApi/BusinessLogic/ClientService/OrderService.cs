﻿using Entity;
using Infrastructure;
using NPOI.SS.Formula.Functions;
using Repository.RepositoryService;
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
        public OrderService(IOrderRepository _orderRepository, IUserPrintsSumRepository _sumRepository,IUserPorintsRecordRepository _recordRepository, IGoodsRepository _goodsRepository)
        {
            orderRepository = _orderRepository;
            sumRepository = _sumRepository;
            recordRepository = _recordRepository;
            goodsRepository = _goodsRepository;
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        public AjaxResult SubmitOrder(OrderInfoEntity order, string userId)
        {
            try
            {
                var entity = new OrderBasicModel();
                //执行存储过程，处理的逻辑
                //1:扣除该用户的积分，类型为余额积分/团队积分，更新到汇总表
                var userPorintsEntity = new UserPrintsSumEntity();
                userPorintsEntity = sumRepository.FindEntity(x => x.UserId == userId);
                if (userPorintsEntity == null)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你的积分余额月不足，请先充值", data = "" };
                }
                int i = 0;
                int payCount = (order.GoodsUnitPrice) * (order.BuyGoodsNums);
                if (order.UsePorintsType == 1)
                {
                    //选择使用积分余额结算
                    int PorintsSurplus = userPorintsEntity.PorintsSurplus;
                    if (PorintsSurplus <= 0)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的积分余额月不足，请先充值", data = "" };
                    }
                    if (PorintsSurplus < payCount)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的积分余额月不足，请先充值", data = "" };
                    }
                    userPorintsEntity.PorintsSurplus = (userPorintsEntity.PorintsSurplus) - (payCount);
                }
                else if (order.UsePorintsType == 2)
                {
                    int TreamPorints = userPorintsEntity.TreamPorints;
                    if (TreamPorints <= 0)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的团队积分余额不足，请用余额购买", data = "" };
                    }
                    if (TreamPorints < payCount)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的团队积分余额不足，请用余额购买", data = "" };
                    }
                    userPorintsEntity.TreamPorints = (userPorintsEntity.TreamPorints) - (payCount);
                }
                i = 0;
                i = sumRepository.Update(userPorintsEntity);
                if (i < 1)
                {
                    return null;
                }
                //2:订单表入库，
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
                orderEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                i = orderRepository.Insert(orderEntity);
                if (i < 1)
                {
                    return null;
                }
                var goods = goodsRepository.FindEntity(x => x.GoodsId == order.GoodsId && x.Enable == "Y");
                if (goods.isProduct == "N")
                {
                    //不对商品进行赠送
                    return new AjaxResult { state = ResultType.success.ToString(), message = "下单成功！", data = "" };
                }
                //0点到21点送积分，大于9点不送分
                var currHour = DateTime.Now.Hour;
                int Begin_PayOderHour = 0;
                int End_PayOderHour = 21;
                if (currHour >= Begin_PayOderHour && currHour <= End_PayOderHour)
                {
                    //赠送
                    //3:购买成功后立即赠送对应的商品积分，添加到用户积分记录表中
                    int ItemPoints = goodsRepository.FindEntity(x => x.GoodsId == order.GoodsId).ItemPoints;
                    int sumItemPoints = ItemPoints * order.BuyGoodsNums;
                    var userPorintsRecord = new UserPorintsRecordEntity();
                    userPorintsRecord.UserId = userId;
                    userPorintsRecord.GoodsId = order.GoodsId;
                    userPorintsRecord.ProductPorints = sumItemPoints;
                    userPorintsRecord.PorintsType = 1;
                    userPorintsRecord.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    recordRepository.Insert(userPorintsRecord);
                    if (i < 1)
                    {
                        return null;
                    }
                    //4:赠送完预购积分后，需要再更新到积分汇总表中的预购积分中去
                    //如果用户没有在汇总表中初始化数据，就进行初始化操作
                    userPorintsEntity = new UserPrintsSumEntity();
                    userPorintsEntity = sumRepository.FindEntity(x => x.UserId == userId && x.GoodsId == order.GoodsId);
                    if (userPorintsEntity == null)
                    {
                        var sumporintsEntity = new UserPrintsSumEntity();
                        sumporintsEntity.UserId = order.UserId;
                        sumporintsEntity.GoodsId = order.GoodsId;
                        sumporintsEntity.ProductPorints = 0;
                        sumporintsEntity.TreamPorints = 0;
                        sumporintsEntity.PorintsSurplus = 0;
                        sumporintsEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        sumRepository.Insert(sumporintsEntity);
                        if (i < 1)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        userPorintsEntity.ProductPorints = (userPorintsEntity.ProductPorints) + (sumItemPoints);
                        sumRepository.Update(userPorintsEntity);
                        if (i < 1)
                        {
                            return null;
                        }
                    }
                }
                return new AjaxResult { state = ResultType.success.ToString(), message = "下单成功！", data = "" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AjaxResult PayPorints(int payNum, string userId)
        {
            var userPorintsEntity = new UserPrintsSumEntity();
            userPorintsEntity = sumRepository.FindEntity(x => x.UserId == userId);
            if (userPorintsEntity == null)
            {
                return null;
            }
            userPorintsEntity.PorintsSurplus = (userPorintsEntity.PorintsSurplus) + (payNum);
            int i = sumRepository.Update(userPorintsEntity);
            if (i < 1)
            {
                return null;
            }
            return new AjaxResult { state = ResultType.success.ToString(), message = "充值成功！", data = "" };
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
