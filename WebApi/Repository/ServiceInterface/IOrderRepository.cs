using Entity;
using RepositoryFactory.RepositorysBase;
using System;
using System.Collections.Generic;
using System.Text;


namespace RepositoryFactory.ServiceInterface
{
    public interface IOrderRepository : IRepository<OrderInfoEntity>
    {
        int GetUser_PayMaxGoodsLeve(string userId);

        bool IsWorkDate(string date);
    }
}
