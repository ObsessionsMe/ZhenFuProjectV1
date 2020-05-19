using Entity;
using Infrastructure.DBContext;
using Repository.ServiceInterface;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.RepositoryService
{
    public class DictionaryRepository : Repository<DictionaryEntity>, IDictionaryRepository
    {
        public DictionaryRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
    }
}
