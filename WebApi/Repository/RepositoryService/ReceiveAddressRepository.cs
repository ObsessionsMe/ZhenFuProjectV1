using Entity;
using Infrastructure.DBContext;
using Repository.RepositoryService;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryFactory.RepositoryService
{
    public class ReceiveAddressRepository : Repository<ReceiveAddressEntity>, IReceiveAddressRepository
    {
        public ReceiveAddressRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
    }
}
