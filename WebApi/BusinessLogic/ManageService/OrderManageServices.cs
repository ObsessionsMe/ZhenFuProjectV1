using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
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
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
            }
            return orderRepository.GetUse_OrderList(pagination, expression);
        }
    }
}
