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
        public GoodsService(IGoodsRepository _goodsRepository)
        {
            goodsRepository = _goodsRepository;
        }

        /// <summary>
        /// 获取所有有效商品
        /// </summary>
        /// <returns></returns>
        public List<GoodsEntity> FindGoodsList()
        {
            return goodsRepository.IQueryable(x => x.Enable == "Y").ToList();
        }

        public GoodsEntity GetGoodsDetails(string goodsId)
        {
            return goodsRepository.FindEntity(x => x.GoodsId == goodsId && x.Enable == "Y");
        }
    }
}
