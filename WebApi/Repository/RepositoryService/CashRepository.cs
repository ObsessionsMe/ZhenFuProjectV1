using Entity;
using Infrastructure;
using Infrastructure.DBContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ViewEntity;

namespace RepositoryFactory.RepositoryService
{
    public class CashRepository : Repository<CashInfoEntity>, ICashRepository
    {
        public CashRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }

        public DataTable GetCashDetail(string userId, int type,string GoodsId)
        {
            var sqlParams = new SqlParams();
            sqlParams.Params.Add("@userId", userId);
            sqlParams.Params.Add("@type", type);
            sqlParams.Params.Add("@goodsId", GoodsId);
            return ExecuteSql.ProcQuery(new DatabaseFacade(dbcontext), "p_get_cash_detail", sqlParams).Tables[0];
        }

        public List<CashListEntity> GetUse_CashList(Pagination pagination, Expression<Func<CashListEntity, bool>> predicate)
        {
            var repositorys = new Repository<CashListEntity>(dbcontext);
            return repositorys.FindList(predicate, pagination);
        }
        public int ApplyUserCash(int type, string cashIds)
        {
            //1:更改兑现状态
            //2:如果是驳回就需要恢复用户的积分
            var sqlParams = new SqlParams();
            //sqlParams.Params.Add("@type", type);
            //sqlParams.Params.Add("@ids", cashIds);
            //string execSql = "p_apply_user_cash";
            string execSql = string.Format(" update zf_cash_info set staus = {0} where id in ( {1} ) ", type, cashIds);
            int i = dbcontext.Database.ExecuteSqlCommand(execSql, null);
            if (i <= 0) {
                return -1;
            }
            execSql = string.Format(" update zf_cash_info set staus = {0} where id in ( {1} ) ", type, cashIds);

            return i;
        }
    }
}