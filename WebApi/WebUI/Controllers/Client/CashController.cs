using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Entity;
using Infrastructure;
using Infrastructure.LogConfig;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public CashController(ICashRepository _CashRepository, IUserPrintsSumRepository _userPrintsSumRepository)
        {
            cashService = new CashService(_CashRepository, _userPrintsSumRepository);
        }

        [Route("getCashDetail")]
        public ActionResult GetCashDetail(int type, string GoodsId)
        {
            var result = new AjaxResult();
            try
            {
                //获取用户的兑现详情
                result.data = cashService.GetCashDetail(userModel.UserId, type, GoodsId).ToDynamics().First();
                result.state = ResultType.success.ToString();
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
        [Route("submitCash")]
        public ActionResult InsertCash(CashInfoEntity entity)
        {
            var flag = true;
            var result = new AjaxResult();
            try
            {
                var currHour = DateTime.Now.Hour;
                if (!(currHour >= BeginHour && currHour <= EndHour))
                {
                    flag = false;
                    result.state = ResultType.error.ToString();
                    result.message = $"提现时间为{BeginHour}:00-{EndHour}:00!";
                }

                if (flag)
                {
                    if (entity.BankUserName != userModel.Name)
                    {
                        flag = false;
                        result.state = ResultType.error.ToString();
                        result.message = "开户人姓名与当前账号用户姓名不一致!";
                    }
                }

                if (flag)
                {
                    entity.UserId = userModel.UserId;
                    entity.Date = DateTime.Now;
                    result.state = ResultType.success.ToString();
                    result = cashService.InsertCashInfo(entity);
                }
            }
            catch (Exception ex)
            {
                result.message = "提交失败!";
                result.state = ResultType.error.ToString();
                LogHelper.Log.Error(ex);
            }
            return Json(result);
        }

    }
}
