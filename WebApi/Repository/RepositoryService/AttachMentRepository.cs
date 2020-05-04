using Entity;
using Infrastructure.DBContext;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.RepositoryService
{
    public class AttachMentRepository : Repository<AttachMentInfoEntity>, IAttachMentRepository
    {
        public AttachMentRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
    }
}
