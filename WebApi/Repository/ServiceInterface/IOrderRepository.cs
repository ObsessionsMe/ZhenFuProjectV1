using Entity;
using Infrastructure;
using RepositoryFactory.RepositorysBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ViewEntity;

namespace RepositoryFactory.ServiceInterface
{
    public interface IOrderRepository : IRepository<OrderInfoEntity>
    {
        int GetUser_PayMaxGoodsLeve(string userId);

        bool IsWorkDate(string date);

        List<OrderListEntity> GetUse_OrderList(Pagination pagination, Expression<Func<OrderListEntity, bool>> predicate);

        bool IsOverStp_PayMaxGoodsLeve(string userId, string goodsId);

        OrderListEntity GetUse_OrderListByOrderNumber(string OrderNumber);
    }
}
