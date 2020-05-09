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
        public CashService(ICashRepository _CashRepository)
        {
            CashRepository = _CashRepository;
        }

        public AjaxResult InsertCashInfo(CashInfoEntity entity)
        {
            AjaxResult result = new AjaxResult();
            CashRepository.Insert(entity);
            result.state = ResultType.success.ToString();
            result.message = "提交成功！";
            return result;
        }

        public DataTable GetCashDetail(string userId, int type, string GoodsId)
        {
            return CashRepository.GetCashDetail(userId,type,GoodsId);
        }
    }
}
