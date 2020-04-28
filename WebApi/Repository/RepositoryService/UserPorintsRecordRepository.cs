using Entity;
using Infrastructure.DBContext;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.RepositoryService
{
    public class UserPorintsRecordRepository : Repository<UserPorintsRecordEntity>, IUserPorintsRecordRepository
    {
        public UserPorintsRecordRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
    }
}
