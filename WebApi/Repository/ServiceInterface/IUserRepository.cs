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
   public  interface IUserRepository: IRepository<UserInfoEntity>
    {
        List<UserPorintListEntity> GetUser_PorintList(Pagination pagination, Expression<Func<UserPorintListEntity, bool>> predicate);

        DataTable GetUserTeamLevel1(string userId, string goodsId);

        DataTable GetUserTeamLevel2(string userId, string goodsId);
    }
}
