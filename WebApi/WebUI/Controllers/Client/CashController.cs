using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Entity;
using Infrastructure;
using Infrastructure.LogConfig;
using Microsoft.AspNetCore.Mvc;
using RepositoryFactory.ServiceInterface;
using WebUI.App_Start;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  用户收货地址，添加/删除/修改
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CashController : BaseControllers
    {
        public CashService cashService;

        public CashController(ICashRepository _CashRepository)
        {
            cashService = new CashService(_CashRepository);
        }

        [Route("getCashDetail")]
        public ActionResult GetCashDetail(int type)
        {
            var result = new AjaxResult();
            try
            {
                //获取
            }
            catch (Exception ex)
            {
                result.message = "提交失败!";
                result.state = ResultType.success.ToString();
                LogHelper.Log.Error(ex);
            }
            return Json(result);
        }


        /// <summary>
        ///  提交兑现
        /// </summary>
        /// <returns></returns>
        [Route("Submit")]
        public ActionResult InsertCash(CashInfoEntity entity)
        {
            var result = new AjaxResult();
            try
            {
                //entity.UserId = userModel.UserId;
                entity.Date = DateTime.Now;
                result = cashService.InsertCashInfo(entity);
            }
            catch (Exception ex)
            {
                result.message = "提交失败!";
                result.state = ResultType.success.ToString();
                LogHelper.Log.Error(ex);
            }
            return Json(result);
        }

    }
}
