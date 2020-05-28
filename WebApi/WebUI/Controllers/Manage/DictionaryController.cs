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

        [Route("GetDicAllList")]
        public AjaxResult<dynamic> GetDictionaryListByPid(int Pid)
        {
            AjaxResult<dynamic> ajaxResult = new AjaxResult<dynamic>();


            ajaxResult.data = dictionaryRepository.FindList(w => w.PId == Pid && w.Enable == "Y").Select(s => new { Name = s.Name, Code = s.Code, Sort = s.Sort, FileName = attachMentRepository.FindEntity(f => f.MainId == s.Code && f.AttachmentType == 5)?.AttachmentName }).OrderBy(o => o.Sort);
            ajaxResult.state = ResultType.success;
            ajaxResult.message = "获取成功";

            return ajaxResult;
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
            Expression<Func<DictionaryEntity, bool>> predicate = b => (Pid != 0 && b.PId == Pid );
            ajaxResult.data = new { rows = dictionaryRepository.FindList(predicate, basePara), records = basePara.records };
            ajaxResult.state = ResultType.success;
            ajaxResult.message = "获取成功";

            return ajaxResult;
        }

        [Route("SubDic")]
        public AjaxResult<int> SubmitDictionary(ExpandoObject obj)
        {
            AjaxResult<int> ajaxResult = new AjaxResult<int>();
            var dataObj = obj as dynamic;
            string fileName = dataObj.fileName.ToString();
            DictionaryEntity entity = Newtonsoft.Json.JsonConvert.DeserializeObject<DictionaryEntity>(dataObj.entity.ToString());

            if (entity.Id > 0)
            {  //编辑
                var oldEntity = dictionaryRepository.FindEntity(entity.Id);
                if (oldEntity != null)
                {
                    oldEntity.Sort = entity.Sort;
                    oldEntity.Name = entity.Name;
                    oldEntity.Enable = entity.Enable;
                    entity = oldEntity;
                    dictionaryRepository.Update(entity);
                }
            }
            else
            {  //新增
                entity.Code = Common.CreateSystemId("GDT");
                entity.AddTime = DateTime.Now;
                entity.AddUserId = "0";
                entity.Id = dictionaryRepository.Insert(entity);
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                attachMentRepository.Delete(d => d.MainId == entity.Code && d.AttachmentType == 5);
                //附件关联
                attachMentRepository.Insert(new AttachMentInfoEntity()
                {
                    MainId = entity.Code,
                    AttachmentType = 5,
                    AttachmentName = fileName,
                    UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

            ajaxResult.data = entity.Id;
            ajaxResult.state = ResultType.success;
            ajaxResult.message = "提交成功";
            return ajaxResult;
        }
    }
}
