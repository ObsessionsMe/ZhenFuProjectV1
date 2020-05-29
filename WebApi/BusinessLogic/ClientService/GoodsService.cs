using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.ClientService
{
    public class GoodsService
    {
        /// <summary>
        ///  商品模块业务逻辑
        /// </summary>
        private IGoodsRepository goodsRepository;
        private IOrderRepository orderRepository;
        public GoodsService(IGoodsRepository _goodsRepository, IOrderRepository _orderRepository)
        {
            goodsRepository = _goodsRepository;
            orderRepository = _orderRepository;
        }

        public List<GoodsEntity> GetGoodsListByProduct()
        {
            return goodsRepository.FindList(f => f.Enable == "Y" & f.isProduct == "Y");
        }

        public List<GoodsEntity> GetGoodsListByType(string type)
        {
            return goodsRepository.GetGoodsListByType(type);
        }

        /// <summary>
        /// 获取所有有效商品
        /// </summary>
        /// <returns></returns>
        public List<GoodsEntity> FindGoodsList()
        {
            return goodsRepository.IQueryable(x => x.Enable == "Y" && x.isProduct == "Y").ToList();
        }

        public GoodsEntity GetGoodsDetails(string goodsId)
        {
            return goodsRepository.FindEntity(x => x.GoodsId == goodsId && x.Enable == "Y");
        }

        //获取用户已经购买过得商品
        public AjaxResult checkGoodLevel(string userId, string goodsId)
        {
            int beforLevel = goodsRepository.FindEntity(x => x.GoodsId == goodsId && x.Enable == "Y" ).GoodsLevel;  //当前要购买的商品级别
            if (beforLevel == 1)
            {
                return new AjaxResult { state = ResultType.success.ToString(), message = "允许购买！", data = "" };
            }
            List<OrderInfoEntity> orderList = orderRepository.FindList(x => x.UserId == userId && x.OrderStatus > 0);
            if (orderList.Count == 0)
            {
                if (beforLevel > 1)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "前先购买第1级商品", data = "" };
                }
            }
            int level = orderRepository.GetUser_PayMaxGoodsLeve(userId);
            //当前级别如果是3，以前只卖过1级产品，那么违规了
            if (Convert.ToInt32(beforLevel - level) > 1)
            {
                return new AjaxResult { state = ResultType.error.ToString(), message = "前先购买第" + (level + 1) + "级商品", data = "" };
            }
            return new AjaxResult { state = ResultType.success.ToString(), message = "允许购买！", data = "" };
        }
    }
}
