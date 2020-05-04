using Entity;
using Entity.Params;
using Infrastructure;
using RepositoryFactory.RepositorysBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace RepositoryFactory.ServiceInterface
{
   public  interface IUserPrintsSumRepository : IRepository<UserPrintsSumEntity>
    {
        public DataSet GetProductEarn(GoodsParam goodsParam);
        public DataSet GetTeamEarn(GoodsParam param);
    }
}
