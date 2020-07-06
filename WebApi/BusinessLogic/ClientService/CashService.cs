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
        public CashService(ICashRepository _CashRepository, IUserPrintsSumRepository _userPrintsSumRepository, IUserPorintsRecordRepository _userPorintsRecordRepository, IUserRepository _userRepository)
        {
            CashRepository = _CashRepository;
            userPrintsSumRepository = _userPrintsSumRepository;
            userPorintsRecordRepository = _userPorintsRecordRepository;
            userRepository = _userRepository;
        }

        public AjaxResult InsertCashInfo(CashInfoEntity entity)
        {
            AjaxResult result = new AjaxResult();
            CashRepository.Insert(entity);
            //积分扣除
            var userEntity=userRepository.FindEntity(f => f.UserId == entity.UserId);
            if (entity.Type == 1)
            {

            }
            else
            {
                userEntity.PecialItemPorints -= entity.Deduct;
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
