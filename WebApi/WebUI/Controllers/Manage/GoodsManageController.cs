using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ManageService;
using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryFactory.ServiceInterface;
using WebUI.Tool;

namespace WebUI.Controllers.Manage
{
    /// <summary>
    /// 商品管理模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsManageController : Controller
    {
        private readonly IGoodsRepository goodsRepository;
        private IHostingEnvironment hostEnvironment;
        public GoodsManageController(IGoodsRepository _goodsRepository, IHostingEnvironment _hostEnvironment)
        {
            goodsRepository = _goodsRepository;
            hostEnvironment = _hostEnvironment;
        }
        // GET: api/GoodsManage/GetGoodsList
        /// <summary>
        ///  获取所有商品信息
        /// </summary>
        /// <returns></returns>
        [Route("GetGoodsList")]
        public ActionResult GetGoodsList(PaginationParam param)
        {
            GoodsManageServices service = new GoodsManageServices(goodsRepository);
            var pagination = param.pagination;
            string keyword = param.keyword;
            var data = new
            {
                rows = service.GetGoodsList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Json(new AjaxResult { state = ResultType.success, message = "获取数据成功", data = data });
        }

        /// <summary>
        /// 提交商品数据(新增/保存)
        /// </summary>
        /// <returns></returns>
        [Route("SubmitGoods")]
        public ActionResult SubmitGoods(GoodsEntity goodsEntity)
        {
            //1:商品基础数据入库
            //2:商品附件入库
            if (goodsEntity == null)
            {
                return Json(new AjaxResult { state = ResultType.error, message = "提交商品失败", data = "" });
            }
            if (string.IsNullOrEmpty(goodsEntity.GoodsId))
            {
                //新增
                goodsEntity.GoodsId = "GD" + Common.CreateNo() + Common.RndNum(5);
                goodsEntity.Enable = "Y";
                goodsEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            }
            else
            {
                //修改
            }
            GoodsManageServices service = new GoodsManageServices(goodsRepository);
            int i = service.SubmitGoodsGoodsEntity(goodsEntity);
            if (i <= 0)
            {
                return Json(new AjaxResult { state = ResultType.error, message = "提交商品失败", data = "" });
            }
            //附件表存储商品Id相关附件，主图和详情图只有一个，商品详情轮播图可能有多个

            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 商品-上传文件接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadGoodsFile")]
        public ActionResult UploadGoodsFile()
        {
            try
            {
                var data = Request.Form.Files["file"];
                if (data == null)
                {
                    return Json(new AjaxResult { state = ResultType.error, message = "上传文件失败", data = "" });
                }
                object obj = Request.Form;
                string fileName = Common.GuId();
                fileName = fileName + ".png";
                string dir = Path.Combine(hostEnvironment.ContentRootPath, "Upload/GoodsImg");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string filePath = Path.Combine(dir, fileName);
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
                using (var stream = System.IO.File.Create(filePath))
                {
                    data.CopyTo(stream);
                }
                return Json(new AjaxResult { state = ResultType.success, message = "上传文件成功", data = "Upload/GoodsImg/" + fileName });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
