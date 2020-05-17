using Entity;
using Infrastructure;
using RepositoryFactory.RepositorysBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using ViewEntity;

namespace RepositoryFactory.ServiceInterface
{
    public interface ICashRepository : IRepository<CashInfoEntity>
    {
        DataTable GetCashDetail(string userId, int type, string GoodsId);

        List<CashListEntity> GetUse_CashList(Pagination pagination, Expression<Func<CashListEntity, bool>> predicate);
    }
}
