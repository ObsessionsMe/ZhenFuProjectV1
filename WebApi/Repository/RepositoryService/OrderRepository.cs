using Entity;
using Infrastructure;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ViewEntity;

namespace RepositoryFactory.RepositoryService
{
    public class OrderRepository : Repository<OrderInfoEntity>, IOrderRepository
    {
        public OrderRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }
        public int GetUser_PayMaxGoodsLeve(string userId)
        {
            List<GoodsEntity> list = ExecuteSql.SqlQuery<GoodsEntity>(new DatabaseFacade(dbcontext), string.Format("exec proc_getUser_payMaxgoodsLeve '{0}'", userId), new object[] { });
            if (list.Count == 0)
            {
                return -1;
            }
            return list[0].GoodsLevel;
        }

        public bool IsWorkDate(string date)
        {
            var dateEntity = dbcontext.WorkDateEntity.FirstOrDefault(x => x.WorkDates == date);
            if (dateEntity == null)
            {
                return false;
            }
            return true;
        }

        public List<OrderListEntity> GetUse_OrderList(Pagination pagination, Expression<Func<OrderListEntity, bool>> predicate)
        {
            //return dbcontext.UserPorintListEntity.ToList();
            var repositorys = new Repository<OrderListEntity>(dbcontext);
            return repositorys.FindList(predicate, pagination);
        }


        public bool IsOverStp_PayMaxGoodsLeve(string userId, string goodsId)
        {
            List<GoodsEntity> list = ExecuteSql.SqlQuery<GoodsEntity>(new DatabaseFacade(dbcontext), string.Format("exec IsOverStp_PayMaxGoodsLeve '{0}','{1}'", userId, goodsId), new object[] { });
            return  true;
        }
    }
}