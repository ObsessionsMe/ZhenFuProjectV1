using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            //var expression = ExtLinq.True<GoodsEntity>();
            //expression = expression.And(t => t.Enable == "Y");
            Expression<Func<GoodsEntity, bool>> expression = b => (b.Enable == "Y" && string.IsNullOrEmpty(keyword)) || (b.Enable == "Y" &&
       !string.IsNullOrEmpty(keyword) && b.GoodsName.Contains(keyword));
            return goodsRepository.FindList(expression, pagination).ToList();
        }

        public int SubmitGoodsGoodsEntity(GoodsEntity goodsEntity)
        {
            return goodsRepository.Insert(goodsEntity);
        }
        public List<GoodsEntity> GetAllProducts()
        {
            return goodsRepository.FindList(t => t.Enable == "Y" && t.isProduct == "Y");
        }
    }
}
