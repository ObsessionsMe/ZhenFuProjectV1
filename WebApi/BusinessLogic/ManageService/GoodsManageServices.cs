using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ManageService
{
   public class GoodsManageServices
    {
        /// <summary>
        ///  商品模块业务逻辑
        /// </summary>
        private IGoodsRepository goodsRepository;
        public GoodsManageServices(IGoodsRepository _goodsRepository)
        {
            goodsRepository = _goodsRepository;
        }
        public List<GoodsEntity> GetGoodsList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<GoodsEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.GoodsName.Contains(keyword));
            }
            return goodsRepository.FindList(expression, pagination);
        }
        public int SubmitGoodsGoodsEntity(GoodsEntity goodsEntity)
        {
            return goodsRepository.Insert(goodsEntity);
        }
    }
}
