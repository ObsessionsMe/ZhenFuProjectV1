using Entity;
using Infrastructure.DBContext;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryFactory.RepositoryService
{
    public class UserRepository : Repository<UserInfoEntity>, IUserRepository
    {
        public UserRepository(ZFDBContext _dbcontext) : base(_dbcontext) {

        }
    }
}