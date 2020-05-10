using Entity;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewEntity;

namespace RepositoryFactory.RepositoryService
{
    public class UserRepository : Repository<UserInfoEntity>, IUserRepository
    {
        public UserRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
        public List<UserPorintListEntity> GetUser_PorintList()
        {
            return dbcontext.UserPorintListEntity.ToList();        
        }

        public DataTable GetUserTeamLevel1(string userId, string goodsId)
        {
            string sql = string.Format(" select * From f_get_user_shop_team( '{0}' , '{1}' ) where Level =1 ", userId, goodsId);
            return ExecuteSql.SqlQuery(new DatabaseFacade(dbcontext), sql);
        }
        public DataTable GetUserTeamLevel2(string userId, string goodsId)
        {
            string sql = string.Format(" select * From f_get_user_shop_team( '{0}' , '{1}' )  where Level =2 ", userId, goodsId);
            return ExecuteSql.SqlQuery(new DatabaseFacade(dbcontext), sql);
        }

    }
}