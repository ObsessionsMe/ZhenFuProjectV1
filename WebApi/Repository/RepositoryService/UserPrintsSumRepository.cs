using Entity;
using Entity.Params;
using Infrastructure;
using Infrastructure.DBContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository.RepositoryService
{
    public class UserPrintsSumRepository : Repository<UserPrintsSumEntity>, IUserPrintsSumRepository
    {
        public UserPrintsSumRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }

        public DataSet GetProductEarn(GoodsParam goodsParam)
        {
            var sqlParams = new SqlParams();
            sqlParams.Params.Add("@userId", goodsParam.UserId);
            sqlParams.Params.Add("@goodsId", goodsParam.GoodsId);
            if (goodsParam.BeginDate.HasValue)
            {
                sqlParams.Params.Add("@begindate", goodsParam.BeginDate);
            }
            if (goodsParam.EndDate.HasValue)
            {
                sqlParams.Params.Add("@enddate", goodsParam.EndDate);
            }
            sqlParams.Params.Add("@pageBeginIndex", goodsParam.pageBegin);
            sqlParams.Params.Add("@pageEndIndex", goodsParam.pageEnd);


            return ExecuteSql.ProcQuery(new DatabaseFacade(dbcontext), "p_get_product_earn", sqlParams);

        }

        public DataSet GetTeamEarn(GoodsParam goodsParam)
        {
            var sqlParams = new SqlParams();
            sqlParams.Params.Add("@userId", goodsParam.UserId);
            sqlParams.Params.Add("@goodsId", goodsParam.GoodsId);
            if (goodsParam.BeginDate.HasValue)
            {
                sqlParams.Params.Add("@begindate", goodsParam.BeginDate);
            }
            if (goodsParam.EndDate.HasValue)
            {
                sqlParams.Params.Add("@enddate", goodsParam.EndDate);
            }
            sqlParams.Params.Add("@pageBeginIndex", goodsParam.pageBegin);
            sqlParams.Params.Add("@pageEndIndex", goodsParam.pageEnd);


            return ExecuteSql.ProcQuery(new DatabaseFacade(dbcontext), "p_get_team_earn", sqlParams);

        }
    }
}
