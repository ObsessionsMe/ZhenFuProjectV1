using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ViewEntity;

namespace BusinessLogic.ManageService
{
    public class OrderManageServices
    {
        /// <summary>
        ///  商品模块业务逻辑
        /// </summary>
        private IOrderRepository orderRepository;
        public OrderManageServices(IOrderRepository _orderRepository)
        {
            orderRepository = _orderRepository;
        }

        public List<OrderListEntity> GetOrderList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<OrderListEntity>();
            expression = expression.And(a => a.Enable == "Y");
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(a => a.GoodsName.Contains(keyword) || a.Name.Contains(keyword));
            }
            //if (beginDate.HasValue)
            //{
            //    expression = expression.And(a => a.AddTime >= beginDate.Value);
            //}

            //if (endDate.HasValue)
            //{
            //    expression = expression.And(a => a.AddTime <= endDate.Value);
            //}
            return orderRepository.GetUse_OrderList(pagination, expression);
        }

    }
}
