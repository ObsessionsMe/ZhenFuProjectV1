using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            //var expression = ExtLinq.True<CashListEntity>();
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    expression = expression.And(t => t.Account.Contains(keyword));
            //}
            var expression = ExtLinq.True<CashListEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = b => b.BankUserName.Contains(keyword);
            }
            return cashRepository.GetUse_CashList(pagination, expression);
        }
        public bool ApplyUserCash(int cashType, string ids)
        {
            int i  = cashRepository.ApplyUserCash(cashType, ids);
            if (i <= 0) {
                return false;
            }
            return true;
        }
    }
}
