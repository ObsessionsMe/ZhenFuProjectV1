﻿using System;
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
using Newtonsoft.Json;
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
        private IAttachMentRepository attachRepository;
        public GoodsManageController(IGoodsRepository _goodsRepository, IHostingEnvironment _hostEnvironment, IAttachMentRepository _attachRepository)
        {
            goodsRepository = _goodsRepository;
            hostEnvironment = _hostEnvironment;
            attachRepository = _attachRepository;
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
        public ActionResult SubmitGoods(string jsonString)
        {
            try
            {
                GoodsEntity goodsEntity = JsonConvert.DeserializeObject<GoodsEntity>(jsonString);
                //2:商品附件入库//1:商品基础数据入库
                if (goodsEntity == null)
                {
                    return Json(new AjaxResult { state = ResultType.error, message = "提交商品失败", data = "" });
                }
                if (string.IsNullOrEmpty(goodsEntity.GoodsId))
                {
                    if (goodsEntity.isProduct == "N")
                    {
                        //新增商品
                        goodsEntity.GoodsId = "GD" + Common.CreateNo() + Common.RndNum(5);
                        goodsEntity.Enable = "Y";
                        goodsEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        goodsEntity.ItemPoints = goodsEntity.IndirectPoints = goodsEntity.DirectPoints = 0;
                        goodsEntity.GoodsLevel = 1;
                        goodsEntity.StockCount = 10000;

                    }
                }
                else
                {
                    //修改
                }
                GoodsManageServices service = new GoodsManageServices(goodsRepository);
                int j = service.SubmitGoodsGoodsEntity(goodsEntity);
                if (j <= 0)
                {
                    return Json(new AjaxResult { state = ResultType.error, message = "提交商品失败", data = "" });
                }
                //附件表存储商品Id相关附件，主图单个和详情图多个，商品详情轮播图可能有多个
                //1添加主图
                AttachMentInfoEntity attach = new AttachMentInfoEntity();
                attach.MainId = goodsEntity.GoodsId;
                attach.AttachmentType = 4;
                attach.AttachmentName = goodsEntity.Exterd1;
                attach.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                attachRepository.Insert(attach);
                if (j <= 0)
                {
                    return Json(new AjaxResult { state = ResultType.error, message = "提交商品失败", data = "" });
                }
                //2添加详情图
                if (goodsEntity.Exterd2.IndexOf(',') == -1)
                {
                    attach = new AttachMentInfoEntity();
                    attach.MainId = goodsEntity.GoodsId;
                    attach.AttachmentType = 2;
                    attach.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    attach.AttachmentName = goodsEntity.Exterd2;
                    attachRepository.Insert(attach);
                }
                else
                {
                    string[] detailsImgs = goodsEntity.Exterd2.Split(',');
                    for (int i = 0; i < detailsImgs.Length; i++)
                    {
                        attach = new AttachMentInfoEntity();
                        attach.MainId = goodsEntity.GoodsId;
                        attach.AttachmentType = 2;
                        attach.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        attach.AttachmentName = detailsImgs[i];
                        attachRepository.Insert(attach);
                    }
                }
                //3添加轮播图
                if (goodsEntity.Exterd3.IndexOf(',') == -1)
                {
                    attach = new AttachMentInfoEntity();
                    attach.MainId = goodsEntity.GoodsId;
                    attach.AttachmentType = 1;
                    attach.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    attach.AttachmentName = goodsEntity.Exterd3;
                    attachRepository.Insert(attach);
                }
                else
                {
                    string[] scrollImgs = goodsEntity.Exterd3.Split(',');
                    for (int i = 0; i < scrollImgs.Length; i++)
                    {
                        attach = new AttachMentInfoEntity();
                        attach.MainId = goodsEntity.GoodsId;
                        attach.AttachmentType = 1;
                        attach.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        attach.AttachmentName = scrollImgs[i];
                        attachRepository.Insert(attach);
                    }
                }
                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
            }
            catch (Exception ex) {
                throw ex;
            }
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
                return Json(new AjaxResult { state = ResultType.success, message = "上传文件成功", data = fileName });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
