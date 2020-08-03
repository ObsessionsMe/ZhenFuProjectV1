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

        public CashController(ICashRepository _CashRepository, IUserPrintsSumRepository _userPrintsSumRepository, IUserPorintsRecordRepository _userRecordRepository, IUserRepository _userRepository,IProductCfgRepository _productCfgRepository, IOrderRepository _orderRepository)
        {
            cashService = new CashService(_CashRepository, _userPrintsSumRepository, _userRecordRepository, _userRepository, _productCfgRepository, _orderRepository);
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



        [Route("getCashList")]
        public ActionResult GetCashList(int? type)
        {
            var result = new AjaxResult();
            try
            {
                //获取用户的兑现列表
                result.data = cashService.GetCashList(userModel.UserId, type).OrderByDescending(o=>o.Date);
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
        [Route("RecentCash")]
        public ActionResult RecentCash()
        {
            var result = new AjaxResult();
            try
            {
                var entity = cashService.RecentCash(userModel.UserId);
                result.state = ResultType.success.ToString();
                if (entity != null)
                {
                    result.data = new
                    {
                        Account = entity.Account,
                        BankType = entity.BankType,
                        BankTypeName = entity.BankTypeName,
                        BankUserName = entity.BankUserName,
                        CityCode = entity.CityCode,
                        CityName = entity.CityName,
                        PayType = entity.PayType,
                        PayTypeName = entity.PayTypeName,
                        ProvinceCode = entity.ProvinceCode,
                        ProvinceName = entity.ProvinceName
                    };
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
                if (!(currHour >= BeginHour && currHour < EndHour))
                {
                    flag = false;
                    result.state = ResultType.error.ToString();
                    result.message = $"提现时间为{BeginHour}:00-{EndHour}:00!";
                }

                if (flag)
                {
                    if (entity.Deduct > (((entity.Integral * entity.DeductRate)/100)*100))
                    {
                        flag = false;
                        result.state = ResultType.error.ToString();
                        result.message = $"不能超过可提现最大比例";
                    }
                }

                //if (flag)
                //{
                //    if (entity.BankUserName != userModel.Name)
                //    {
                //        flag = false;
                //        result.state = ResultType.error.ToString();
                //        result.message = "开户人姓名与当前账号用户姓名不一致!";
                //    }
                //}

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
