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
        public DataSet GetTeamEarnDetail(GoodsParam param);
        public DataSet GetTeamDetail(GoodsParam goodsParam);
        public bool IsAgency(GoodsParam goodsParam);
        public DataSet GetTeamOrderList(GoodsParam param);
    }
}
