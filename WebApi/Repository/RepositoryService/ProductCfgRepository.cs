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
    public class ProductCfgRepository: Repository<ProductCfgEntity>, IProductCfgRepository
    {
        public ProductCfgRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
    }
}