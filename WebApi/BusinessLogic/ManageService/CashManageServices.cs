using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using ViewEntity;

namespace BusinessLogic.ManageService
{
   public class CashManageServices
    {
        /// <summary>
        ///  商品模块业务逻辑
        /// </summary>
        private ICashRepository cashRepository;
        public CashManageServices(ICashRepository _cashRepository)
        {
            cashRepository = _cashRepository;
        }
        public List<CashListEntity> GetCashList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<CashListEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Account.Contains(keyword));
            }
            return cashRepository.GetUse_CashList(pagination, expression);
        }
        public bool UserCashApply(int cashType, string ids)
        {
            bool bo = true;
            string[] cashIds = ids.Split(',');
            if (cashType == 1)
            {
                //审批通过，直接更新状态
                for (int i = 0; i < cashIds.Length; i++)
                {
                    int Id = Convert.ToInt32(cashIds[i]);
                    var entity = cashRepository.FindEntity(Id);
                    entity.Status = 1;
                    cashRepository.Update(entity);
                }
            }
            else if (cashType == 2) { 
                  //审批驳回

            }
            return bo;
        }
    }
}
