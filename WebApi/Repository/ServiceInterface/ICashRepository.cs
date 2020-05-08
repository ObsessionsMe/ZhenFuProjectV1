using Entity;
using RepositoryFactory.RepositorysBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace RepositoryFactory.ServiceInterface
{
    public interface ICashRepository : IRepository<CashInfoEntity>
    {
        DataTable GetCashDetail(string userId, int type, string GoodsId);
    }
}
