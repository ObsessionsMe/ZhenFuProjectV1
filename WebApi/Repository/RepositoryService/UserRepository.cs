using Entity;
using Infrastructure;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
    public class UserRepository : Repository<UserInfoEntity>, IUserRepository
    {
        public UserRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
        public List<UserPorintListEntity> GetUser_PorintList(Pagination pagination, Expression<Func<UserPorintListEntity, bool>> predicate)
        {
            //return dbcontext.UserPorintListEntity.ToList();
            var repositorys = new Repository<UserPorintListEntity>(dbcontext);
            return repositorys.FindList(predicate, pagination);
        }

        public DataTable GetUserTeamLevel(string userId)
        {
            string sql = string.Format(" select * From f_get_user_team( '{0}') order by Level", userId);
            return ExecuteSql.SqlQuery(new DatabaseFacade(dbcontext), sql);
        }

        public DataTable GetUserTeamLevel1(string userId, string goodsId)
        {
            string sql = string.Format(" select * From f_get_user_team( '{0}' , '{1}' )  where Level =1 ", userId, goodsId);
            return ExecuteSql.SqlQuery(new DatabaseFacade(dbcontext), sql);
        }

        public DataTable GetUserTeamLevel2(string userId, string goodsId)        
        {
            string sql = string.Format(" select * From f_get_user_team( '{0}' , '{1}' )  where Level =2 ", userId, goodsId);
            return ExecuteSql.SqlQuery(new DatabaseFacade(dbcontext), sql);
        }

        //查询某个会员的团队总持仓盒数
        public object GetUserTeamPayTotal(string userId, string goodsId)
        {
            string sql = string.Format(" select sum(payNum) From f_get_user_team( '{0}' , '{1}' )  where Level = 1 or Level = 2 ", userId, goodsId);
           return dbcontext.Database.SqlQuery<object>(sql).FirstOrDefault();
        } 
    }
}