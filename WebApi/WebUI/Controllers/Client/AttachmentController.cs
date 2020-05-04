using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryFactory.ServiceInterface;
using WebUI.App_Start;

namespace WebUI.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : BaseControllers
    {
        private IAttachMentRepository attachMentRepository;
        public AttachmentController(IAttachMentRepository _attachMentRepository)
        {
            attachMentRepository = _attachMentRepository;
        }
        /// <summary>
        ///  获取附件列表
        /// </summary>
        /// <returns></returns>
        [Route("GetAttachmentListByType")]
        public ActionResult GetAttachmentListByType(int type)
        {
            //获取轮播图和产品列表
            AttachmentService servers = new AttachmentService(attachMentRepository);
            var data = servers.FindAttachMentList(type);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "附件数据为空", data = "" });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = data });
        }
    }
}