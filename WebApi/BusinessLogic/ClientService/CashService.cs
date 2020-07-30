using Entity;
using Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessLogic.ClientService
{

    public class CashService
    {
        /// <summary>
        ///  商品模块业务逻辑
        /// </summary>
        private ICashRepository CashRepository;
        private IUserPrintsSumRepository userPrintsSumRepository;
        private IUserPorintsRecordRepository userPorintsRecordRepository;
        private IUserRepository userRepository;
        private IProductCfgRepository productCfgRepository;
        private IOrderRepository orderRepository;
        public CashService(ICashRepository _CashRepository, IUserPrintsSumRepository _userPrintsSumRepository, IUserPorintsRecordRepository _userPorintsRecordRepository, IUserRepository _userRepository, IProductCfgRepository _productCfgRepository, IOrderRepository _orderRepository)
        {
            CashRepository = _CashRepository;
            userPrintsSumRepository = _userPrintsSumRepository;
            userPorintsRecordRepository = _userPorintsRecordRepository;
            userRepository = _userRepository;
            productCfgRepository = _productCfgRepository;
            orderRepository = _orderRepository;
        }

        public AjaxResult InsertCashInfo(CashInfoEntity entity)
        {
            AjaxResult result = new AjaxResult();
            CashRepository.Insert(entity);
            //积分扣除
            var userEntity=userRepository.FindEntity(f => f.UserId == entity.UserId);
            if (entity.Type == 1)
            {
                //个人福豆
                userEntity.PecialItemPorints -= entity.Deduct;
            }
            else if (entity.Type == 2)
            {
                //团队福豆
                userEntity.TreamPorints -= entity.Deduct;
                //当日不得提现超过4500*盒数
                var  list = productCfgRepository.FindList(x => x.AlgorithmVersion == "v2.0.0");
                if (list.Count > 0)
                {
                    int total = 0;
                    foreach (var item in list)
                    {
                        total += orderRepository.FindList(x => x.GoodsId == item.GoodsId && x.UserId == entity.UserId).Sum(key => key.BuyGoodsNums) * 4500;
                    }
                    if (entity.Deduct > total)
                    {
                        result.state = ResultType.error.ToString();
                        result.message = "团队福豆提现超出限额！";
                        return result;
                    }
                }
            }
            else if (entity.Type == 3)
            {
                //福豆田清0
                userEntity.FieldsPorints = 0;
            }
            userRepository.Update(userEntity);
            //var sumEntity = userPrintsSumRepository.FindEntity(f => f.GoodsId == entity.GoodsId && f.UserId == entity.UserId);
            //if (entity.Type == 1)
            //{
            //    sumEntity.ProductPorints -= entity.Deduct;
            //}
            //else
            //{
            //    sumEntity.TreamPorints -= entity.Deduct;
            //}
            //userPrintsSumRepository.Update(sumEntity);
            //扣除记录
            //userPorintsRecordRepository.Insert(new UserPorintsRecordEntity()
            //{
            //    UserId = entity.UserId,
            //    TreamPorints = entity.Type == 2 ? entity.Deduct : 0,
            //    ProductPorints = entity.Type == 1 ? entity.Deduct : 0,
            //    PorintsType = 2,
            //    Addtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //});


            result.state = ResultType.success.ToString();
            result.message = "提交成功！";
            return result;
        }

        public CashInfoEntity RecentCash(string UserId)
        {
            return CashRepository.FindList(f => f.UserId == UserId)?.OrderByDescending(o => o.Id).FirstOrDefault();
        }

        public DataTable GetCashDetail(string userId, int type, string GoodsId)
        {
            return CashRepository.GetCashDetail(userId, type, GoodsId);
        }
    }
}
