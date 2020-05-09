using Entity;
using RepositoryFactory.RepositorysBase;
using System;
using System.Collections.Generic;
using System.Text;


namespace RepositoryFactory.ServiceInterface
{
    public interface IGoodsRepository : IRepository<GoodsEntity>
    {
        List<GoodsEntity> GetGoodsListByType(int type);
    }
}
