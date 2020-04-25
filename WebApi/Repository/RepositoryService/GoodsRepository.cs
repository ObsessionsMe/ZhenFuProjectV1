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
    public class GoodsRepository : Repository<GoodsEntity>, IGoodsRepository
    {
        public GoodsRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }

    }
}