using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repository.ServiceInterface;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public DictionaryController(IDictionaryRepository _dictionaryRepository)
        {
            dictionaryRepository = _dictionaryRepository;
        }

        [Route("GetDicList")]
        public AjaxResult<List<DictionaryEntity>> GetDictionaryListByPid(BasePara basePara)
        {
            AjaxResult<List<DictionaryEntity>> ajaxResult = new AjaxResult<List<DictionaryEntity>>();

            var Pid = 0;
            if (basePara.KeyVals.Keys.Contains("pid"))
            {
                Pid = basePara.KeyVals["pid"].ToInt();
            }
            Expression<Func<DictionaryEntity, bool>> predicate = b => (Pid != 0 && b.PId == Pid);
            ajaxResult.data = dictionaryRepository.FindList(predicate, basePara);
            ajaxResult.state = ResultType.success;
            ajaxResult.message = "获取成功";

            return ajaxResult;
        }

        [Route("AddDic")]
        public AjaxResult<int> InsertDictionary(DictionaryEntity entity)
        {
            AjaxResult<int> ajaxResult = new AjaxResult<int>();
            ajaxResult.data = dictionaryRepository.Insert(entity);
            ajaxResult.state = ResultType.success;
            ajaxResult.message = "新增成功";
            return ajaxResult;
        }
    }
}
