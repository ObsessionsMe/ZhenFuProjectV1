using Entity;
using Infrastructure;
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
        public AjaxResult SubmitOrder(OrderInfoEntity order, string  userId)
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
                orderEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                i =  orderRepository.Insert(orderEntity);
                if (i < 1) {
                    return null;
                }
                //5:减少对应的商品表库存(支付失败不入库)--暂时放一放
                //3:购买成功后立即赠送对应的商品积分，添加到用户积分记录表中
                int ItemPoints = goodsRepository.FindEntity(x => x.GoodsId == order.GoodsId).ItemPoints;
                int sumItemPoints = ItemPoints * payCount;
                var userPorintsRecord = new UserPorintsRecordEntity();
                userPorintsRecord.UserId = userId;
                userPorintsRecord.ProductPorints = sumItemPoints;
                userPorintsRecord.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                recordRepository.Insert(userPorintsRecord);
                if (i < 1)
                {
                    return null;
                }
                //4:购买成功后立即赠送对应的商品积分，添加到用户积分汇总表中
                return new AjaxResult { state = ResultType.success.ToString(), message = "下单成功！", data = "" };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
