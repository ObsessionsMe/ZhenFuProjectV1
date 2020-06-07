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
   public  interface IProductCfgRepository : IRepository<ProductCfgEntity>
    {

    }
}
