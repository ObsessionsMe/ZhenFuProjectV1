using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using BusinessLogic.ClientService;
using BusinessLogic.ManageService;
using Entity;
using Infrastructure;
using Infrastructure.LogConfig;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using RepositoryFactory.ServiceInterface;
using UtilitieEntity;
using WebUI.App_Start;
using WxPayAPI;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  微信支付-预下单
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeiXinPayNotifyController : Controller
    {
        private readonly IUserRepository userRepository;
        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        public WeiXinPayNotifyController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        /// <summary>
        /// 微信支付-预下单方法
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="payNum"></param>
        /// <returns></returns>
        [Route("ReadyWeiXinPayOrder")]
        public ActionResult ReadyWeiXinPayOrder(string userId, int payNum,string openid)
        {
            if (payNum <= 0)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "充值金额有误，请重新输入", data = "" });
            }
            if (string.IsNullOrEmpty(openid))
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "获取用户openId有误，请登录重试", data = "" });
            }
            JsApiPay jsApiPay = new JsApiPay();
            jsApiPay.openid = openid;
            jsApiPay.total_fee = payNum;
            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                //在页面上显示订单信息
                LogHelper.Log.Error($"WeiXinPayPorints-微信预下单返回结果:{ wxJsApiParam}");
                LogHelper.Log.Error($"WeiXinPayPorints-微信预下单成功:{  unifiedOrderResult.ToPrintStr() }");
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"WeiXinPayPorints-微信预下单出现异常:{ex}");
                throw ex;
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "下单成功", data = wxJsApiParam });
        }

        /// <summary>
        ///  接受微信支付异步通知
        /// </summary>
        /// <returns></returns>
        [Route("AsyWeiXinPayNotifyInfo")]
        public void AsyWeiXinPayNotifyInfo()
        {
            LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-Quray:接受微信支付异步通知进来了");
        }

        /// <summary>
        /// 微信支付已成功，给用户充值积分
        /// </summary>
        [Route("saveWeiXinPayOrderInfo")]
        public ActionResult saveWeiXinPayOrderInfo(string userId, string prepayId, int payNum)
        {
            try
            {
                //存日志
                LogHelper.Log.Error($"微信支付已成功了，并且进入后台开始存储用户积分，用户编号为：{userId}");
                LogHelper.Log.Error($"微信支付已成功了，并且进入后台开始存储用户积分，充值金额为：{payNum}");
                LogHelper.Log.Error($"微信支付已成功了，并且进入后台开始存储用户积分，预下单编号为：{prepayId}");
                //交易成功，给用户充值积分
                if (payNum <= 1) {
                    payNum = 1;
                }
                UserManageService service = new UserManageService(userRepository);
                decimal payNums = Convert.ToDecimal(payNum);
                var data = service.PayPorints(payNums, userId);
                if (data == null)
                {
                    LogHelper.Log.Error($"AsyAliPayNotifyInfo-积分保存失败:{data}");
                    return Json(new AjaxResult { state = ResultType.error.ToString(), message = "充值失败", data = null });
                }
                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "充值成功", data = null });
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"AsyAliPayNotifyInfo-积分保存失败:{ex}");
                throw ex;
            }
        }

        /// <summary>
        /// 通过code获取
        /// </summary>
        [Route("GetOpenIdByCode")]
        public ActionResult GetOpenIdByCode(string code)
        {
            try
            {
                //通过code获取token和用户openId(静默获取)
                string appId = WxPayConfig.GetConfig().GetAppID();
                string appSecret = WxPayConfig.GetConfig().GetAppSecret();
                string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appId, appSecret, code);
                var TokenInfo = new TokenInfo();
                using (var client = new HttpClient())
                {
                    var responseString = client.GetStringAsync(url);
                    LogHelper.Log.Error($"GetOpenIdByCode-获取token返回参数-responseString:{responseString.Result}");
                    TokenInfo = JsonConvert.DeserializeObject<TokenInfo>(responseString.Result);
                    if (TokenInfo == null)
                    {
                        return Json(new AjaxResult { state = ResultType.success.ToString(), message = "用户openId失败", data = null });
                    }
                    return Json(new AjaxResult { state = ResultType.success.ToString(), message = "充值成功", data = TokenInfo.openid });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"GetOpenIdByCode-获取用户openId失败:{ex}");
                throw ex;
            }
       
        }
    }
}
