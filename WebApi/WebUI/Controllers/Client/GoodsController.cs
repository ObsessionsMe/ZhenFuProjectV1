using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using RepositoryFactory.ServiceInterface;
using UtilitieEntity;
using WebUI.App_Start;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  商品模块：首页商品展示/商品详情
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : BaseControllers
    {
        private readonly IGoodsRepository goodsRepository;
        private readonly IOrderRepository orderRepository;
        public GoodsController(IGoodsRepository _goodsRepository, IOrderRepository _orderRepository)
        {
            goodsRepository = _goodsRepository;
            orderRepository = _orderRepository;
        }
        /// <summary>
        ///  获取所有商品列表-首页-轮播图，以及商品列表
        /// </summary>
        /// <returns></returns>
        [Route("GetGoodsList")]
        public ActionResult GetGoodsList()
        {
            //2获取商品，按照轮播图和商品分为两个集合
            GoodsService servers = new GoodsService(goodsRepository, orderRepository);
            var data = servers.FindGoodsList();
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "商品数据为空", data = "" });
            }
            List<GoodsViewTypeModel> CarouselDataList = new List<GoodsViewTypeModel>();
            List<GoodsViewTypeModel> ShopDataList = new List<GoodsViewTypeModel>();
            foreach (var item in data)
            {
                var CarouselData = new GoodsViewTypeModel();
                CarouselData.GoodsId = item.GoodsId;
                CarouselData.GoodsMainImg = item.GoodsMainImg;
                CarouselDataList.Add(CarouselData);
                var ShopData = new GoodsViewTypeModel();
                ShopData.GoodsId = item.GoodsId;
                ShopData.GoodsName = item.GoodsName;
                ShopData.GoodsDescribe = item.GoodsDescribe;
                ShopData.UnitPrice = item.UnitPrice;
                ShopData.GoodsMainImg = item.GoodsMainImg;
                ShopData.GoodsDetailsImg = item.GoodsDetailsImg;
                ShopDataList.Add(ShopData);
            }
            var result = new
            {
                CarouselDataList = CarouselDataList,
                ShopDataList = ShopDataList
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = result });
        }

        /// <summary>
        /// 获取商品详情，
        /// </summary>
        /// <returns></returns>
        [Route("GetGoodsDetails")]
        public ActionResult GetGoodsDetails(string goodsId)
        {
            //2取商品
            GoodsService servers = new GoodsService(goodsRepository, orderRepository);
            var data = servers.GetGoodsDetails(goodsId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "商品详情数据为空", data = "" });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = data });
        }

        [Route("checkGoodLevel")]
        public ActionResult checkGoodLevel(string goodsId)
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败，请重新登录", data = "" });
            }
            GoodsService servers = new GoodsService(goodsRepository, orderRepository);
            var data = servers.checkGoodLevel(userModel.UserId, goodsId);
            return Json(data);
        }

        //上传图片

    }
}
