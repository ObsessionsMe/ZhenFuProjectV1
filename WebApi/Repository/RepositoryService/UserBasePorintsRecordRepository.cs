using Entity;
using Infrastructure.DBContext;
using Repository.ServiceInterface;
using RepositoryFactory.RepositorysBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.RepositoryService
{
    public class UserBasePorintsRecordRepository : Repository<UserBasePorintsRecordEntity>, IUserBasePorintsRecordRepository
    {
        public UserBasePorintsRecordRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {


        }
    }
}
