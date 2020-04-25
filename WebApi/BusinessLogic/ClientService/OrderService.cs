using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtilitieEntity;

namespace BusinessLogic.ClientService
{
    public class OrderService
    {
        /// <summary>
        ///  订单模块业务逻辑
        /// </summary>
        private IOrderRepository goodsRepository;
        public OrderService(IOrderRepository _goodsRepository)
        {
            goodsRepository = _goodsRepository;
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        public  OrderBasicModel SubmitOrder(OrderInfoEntity order)
        {
            var entity = new OrderBasicModel();
            //执行存储过程，处理的逻辑
            //1:订单表入库，
            //2:减少对应的商品表库存(支付失败不入库)
            //3:
            return entity;
        }
    }
}
