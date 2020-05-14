using Entity;
using Infrastructure;
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
        public CashService(ICashRepository _CashRepository, IUserPrintsSumRepository _userPrintsSumRepository)
        {
            CashRepository = _CashRepository;
            userPrintsSumRepository = _userPrintsSumRepository;
        }

        public AjaxResult InsertCashInfo(CashInfoEntity entity)
        {
            AjaxResult result = new AjaxResult();
            CashRepository.Insert(entity);
            //积分扣除
            var sumEntity = userPrintsSumRepository.FindEntity(f => f.GoodsId == entity.GoodsId && f.UserId == entity.UserId);
            if (entity.Type == 1)
            {
                sumEntity.ProductPorints -= entity.Deduct;
            }
            else
            {
                sumEntity.TreamPorints -= entity.Deduct;
            }
            userPrintsSumRepository.Update(sumEntity);

            result.state = ResultType.success.ToString();
            result.message = "提交成功！";
            return result;
        }

        public DataTable GetCashDetail(string userId, int type, string GoodsId)
        {
            return CashRepository.GetCashDetail(userId, type, GoodsId);
        }
    }
}
