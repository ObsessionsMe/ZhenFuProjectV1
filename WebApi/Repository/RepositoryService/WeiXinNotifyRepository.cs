using Entity;
using Infrastructure.DBContext;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.RepositoryService
{
    public class WeiXinNotifyRepository : Repository<WeiXinNotify_Entity>, IWeiXinNotifyRepository
    {
        public WeiXinNotifyRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
    }
}
