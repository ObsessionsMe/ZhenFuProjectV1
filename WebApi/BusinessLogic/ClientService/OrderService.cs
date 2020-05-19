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
        public OrderService(IOrderRepository _orderRepository, IUserPrintsSumRepository _sumRepository,
            IUserPorintsRecordRepository _recordRepository, IGoodsRepository _goodsRepository,IUserRepository _userRepository,
            IUserBasePorintsRecordRepository _basePorintRepository)
        {
            orderRepository = _orderRepository;
            sumRepository = _sumRepository;
            recordRepository = _recordRepository;
            goodsRepository = _goodsRepository;
            userRepository = _userRepository;
            basePorintRepository = _basePorintRepository;
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        public AjaxResult SubmitOrder(OrderInfoEntity order, string userId)
        {
            try
            {
                //执行存储过程，处理的逻辑
                //1:扣除该用户的积分，类型为余额积分/团队积分，专项积分
                 var userEntity = userRepository.FindEntity(x => x.UserId == userId && x.Enable == "Y");
                var sumEntity = new UserPrintsSumEntity();
                if (userEntity == null)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你账号无效，请使用其他账号登录", data = "" };
                }
                 int i = 0;
                int payCount = (order.GoodsUnitPrice) * (order.BuyGoodsNums);  //下单总价
                //减去运费
                if (order.GoodsFreight > 0)
                {
                    payCount = payCount - order.GoodsFreight;
                }
                if (order.UsePorintsType == 1)
                {
                    //积分余额结算
                    int PorintsSurplus = userEntity.PorintsSurplus;
                    if (PorintsSurplus <= 0)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的积分余额月不足，请先充值", data = "" };
                    }
                    if (PorintsSurplus < payCount)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的积分余额月不足，请先充值", data = "" };
                    }
                    userEntity.PorintsSurplus = (userEntity.PorintsSurplus) - (payCount);
                }
                else if (order.UsePorintsType == 2)
                {
                    //团队积分结算
                    sumEntity = sumRepository.FindEntity(x => x.UserId == userId && x.GoodsId == order.GoodsId);
                    //if (sumEntity == null)
                    //{
                    //    var sumporintsEntity = new UserPrintsSumEntity();
                    //    sumporintsEntity.UserId = userId;
                    //    sumporintsEntity.GoodsId = order.GoodsId;
                    //    sumporintsEntity.ProductPorints = 0;
                    //    sumporintsEntity.TreamPorints = 0;
                    //    sumporintsEntity.PorintsSurplus = 0;
                    //    sumporintsEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    //    sumRepository.Insert(sumporintsEntity);
                    //    if (i < 1)
                    //    {
                    //        return null;
                    //    }
                    //}
                    int TreamPorints = sumEntity.TreamPorints;
                    if (TreamPorints <= 0)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的团队积分余额不足，请用余额购买", data = "" };
                    }
                    if (TreamPorints < payCount)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的团队积分余额不足，请用余额购买", data = "" };
                    }
                    sumEntity.TreamPorints = (sumEntity.TreamPorints) - (payCount);
                    i = sumRepository.Update(sumEntity);
                    if (i < 1)
                    {
                        return null;
                    }
                }
                else if (order.UsePorintsType == 3)
                {
                    //专项积分结算
                    int PecialItemPorints = userEntity.PecialItemPorints;
                    if (PecialItemPorints <= 0)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的专项积分不足，请先充值", data = "" };
                    }
                    if (PecialItemPorints < payCount)
                    {
                        return new AjaxResult { state = ResultType.error.ToString(), message = "你的专项积分不足，请先充值", data = "" };
                    }
                    userEntity.PecialItemPorints = (userEntity.PecialItemPorints) - (payCount);

                }
                i = userRepository.Update(userEntity);//扣除积分
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
                    //不对商品进行赠送，只对产品进行赠送
                    return new AjaxResult { state = ResultType.success.ToString(), message = "下单成功！", data = "" };
                }
                //赠送条件 //日期为工作日并且时间为0点到21点
                string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
                bool isWorkDate = orderRepository.IsWorkDate(nowDate);
                if (!isWorkDate)
                {
                    //非工作日
                    return new AjaxResult { state = ResultType.success.ToString(), message = "下单成功！", data = "" };
                }
                 var currHour = DateTime.Now.Hour;
                int Begin_PayOderHour = 0;
                int End_PayOderHour = 21;
                var sum = new UserPrintsSumEntity();
                if (currHour <= Begin_PayOderHour && currHour >= End_PayOderHour)
                {
                    //不在时间范围，不赠送积分，0点到21点送积分，大于9点不送分
                    return new AjaxResult { state = ResultType.success.ToString(), message = "下单成功！", data = "" };
                }
                //3:更新到相关的记录表和
               if(!SavePorintsRecord(order, userEntity))
                {
                    return null;
                }
                return new AjaxResult { state = ResultType.success.ToString(), message = "下单成功！", data = "" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //赠送完积分后，添加到对应记录，以及汇总
        public bool SavePorintsRecord(OrderInfoEntity order, UserInfoEntity userEntity)
        {
            try
            {
                //判断交易类型，如果专项积分或余额就记录到基础积分明细表，否则扣除团队积分
                int i = 0;
                var basePorintEntity = new UserBasePorintsRecordEntity();
                int ItemPoints = goodsRepository.FindEntity(x => x.GoodsId == order.GoodsId).ItemPoints;
                int sumItemPoints = 0;
                if (order.UsePorintsType == 1 || order.UsePorintsType == 3)
                {
                    //专项/余额
                    basePorintEntity.UserId = userEntity.UserId;
                    basePorintEntity.MainId = order.GoodsId;
                    basePorintEntity.OpreateType = 2;
                    basePorintEntity.PorintsType = order.UsePorintsType;
                     basePorintEntity.PorintsSurplus = ItemPoints * order.BuyGoodsNums;
                    basePorintEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    i = basePorintRepository.Insert(basePorintEntity);
                    if (i < 1)
                    {
                        return false;
                    }
                }
                else
                {
                    //团队
                    sumItemPoints = ItemPoints * order.BuyGoodsNums;
                    var userPorintsRecord = new UserPorintsRecordEntity();
                    userPorintsRecord.UserId = userEntity.UserId;
                    userPorintsRecord.GoodsId = order.GoodsId;
                    userPorintsRecord.ProductPorints = sumItemPoints;
                    userPorintsRecord.PorintsType = 1;
                    userPorintsRecord.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    recordRepository.Insert(userPorintsRecord);
                    if (i < 1)
                    {
                        return false;
                    }
                }
                return true;
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
