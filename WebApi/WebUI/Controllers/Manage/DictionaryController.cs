using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repository.ServiceInterface;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebUI.App_Start;

namespace WebUI.Controllers.Manage
{


    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : Controller
    {
        private readonly IDictionaryRepository dictionaryRepository;
        private readonly IAttachMentRepository attachMentRepository;
        public DictionaryController(IDictionaryRepository _dictionaryRepository, IAttachMentRepository _attachMentRepository)
        {
            dictionaryRepository = _dictionaryRepository;
            attachMentRepository = _attachMentRepository;
        }

        [Route("GetDicList")]
        public AjaxResult<dynamic> GetDictionaryListByPid(BasePara basePara)
        {
            AjaxResult<dynamic> ajaxResult = new AjaxResult<dynamic>();

            var Pid = 0;
            if (basePara.KeyVals.Keys.Contains("pid"))
            {
                Pid = basePara.KeyVals["pid"].ToInt();
            }
            Expression<Func<DictionaryEntity, bool>> predicate = b => (Pid != 0 && b.PId == Pid);
            ajaxResult.data = new { rows = dictionaryRepository.FindList(predicate, basePara), records = basePara.records };
            ajaxResult.state = ResultType.success;
            ajaxResult.message = "获取成功";

            return ajaxResult;
        }

        [Route("AddDic")]
        public AjaxResult<int> InsertDictionary(ExpandoObject obj)
        {
            AjaxResult<int> ajaxResult = new AjaxResult<int>();
            var dataObj = obj as dynamic;
            string fileName = dataObj.fileName.ToString();
            DictionaryEntity entity = Newtonsoft.Json.JsonConvert.DeserializeObject<DictionaryEntity>(dataObj.entity.ToString());

            entity.Code = Common.CreateSystemId("GDT");
            entity.AddTime = DateTime.Now;
            entity.AddUserId = "0";
            entity.Id = dictionaryRepository.Insert(entity);

            //附件关联
            attachMentRepository.Insert(new AttachMentInfoEntity()
            {
                MainId = entity.Code,
                AttachmentType = 5,
                AttachmentName = fileName,
                UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            ajaxResult.data = entity.Id;
            ajaxResult.state = ResultType.success;
            ajaxResult.message = "新增成功";
            return ajaxResult;
        }
    }
}
