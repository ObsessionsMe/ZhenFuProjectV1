using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
        private readonly IWeiXinNotifyRepository weiXinNotify;
        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        public WeiXinPayNotifyController(IUserRepository _userRepository, IWeiXinNotifyRepository _weiXinNotify)
        {
            userRepository = _userRepository;
            weiXinNotify = _weiXinNotify;
        }

        /// <summary>
        /// 微信支付-预下单方法
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="payNum"></param>
        /// <returns></returns>
        [Route("ReadyWeiXinPayOrder")]
        public ActionResult ReadyWeiXinPayOrder(string userId, int payNum, string openid)
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
            jsApiPay.total_fee = Convert.ToInt32(payNum * 100);
            jsApiPay.out_trade_no = WxPayApi.GenerateOutTradeNo();
            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数  
                LogHelper.Log.Error($"WeiXinPayPorints-微信预下单返回结果:{ wxJsApiParam}");
                //存储预下单信息
                WeiXinNotify_Entity wXentity = new WeiXinNotify_Entity();
                wXentity.userId = userId;
                wXentity.orderNumber = jsApiPay.out_trade_no;//商户订单号
                wXentity.payNum = payNum;
                wXentity.orderStatus = 0;
                wXentity.notifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int i = weiXinNotify.Insert(wXentity);
                if (i <= 0)
                {
                    return Json(new AjaxResult { state = ResultType.error.ToString(), message = "保存用户预下单信息失败", data = "" });
                }
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
        ///
        [HttpPost]
        [Route("AsyWeiXinPayNotifyInfo")]
        public void AsyWeiXinPayNotifyInfo()
        {
            LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-Quray:接受微信支付异步通知进来了");
            //1接受微信异步通知中的订单编号
            StringBuilder stringBuilder = ReadBodyResult();
            //LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-Request str:{stringBuilder.ToString()}");
            string orderId = new ResultNotify().ProcessNotify(stringBuilder);
            LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-Request orderId:{orderId}");
            if (string.IsNullOrEmpty(orderId))
            {
                LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-Quray:订单编号有问题或者为空");
                return;
            }
            //2判断订单编号是否库中是否存在，并且处是已支付状态，如果存在就表示已经成功处理了，不再处理
            var weiXinEntity = weiXinNotify.FindEntity(x => x.orderNumber == orderId);
            if (weiXinEntity == null)
            {
                //未下单
                LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-2记录充值记录保存时weiXinEntity NULL:{weiXinEntity.ToJson()}");
                return;
            }
            if (weiXinEntity.orderStatus == 1)
            {
                //已下单并充值了
                return;
            }
            if (weiXinEntity.orderStatus == 0)
            {
                weiXinEntity.orderStatus = 1;
                weiXinEntity.notifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int i = weiXinNotify.Update(weiXinEntity);
                if (i <= 0)
                {
                    LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-2记录充值记录保存时失败:{weiXinEntity.ToJson()}");
                    return;
                }
                //3交易成功，给用户充值积分
                UserManageService service = new UserManageService(userRepository);
                decimal payNum = Convert.ToDecimal(weiXinEntity.payNum);
                LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-积分保存参数payNum:{payNum}");
                var data = service.PayPorints(payNum, weiXinEntity.userId);
                if (data == null)
                {
                    LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-积分保存失败:{data}");
                    return;
                }
                LogHelper.Log.Error($"AsyWeiXinPayNotifyInfo-积分保存成功:{data}");
                return;
            }
        }

        public StringBuilder ReadBodyResult()
        {
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            Stream s = Request.Body;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();
            return builder;
        }

        /// <summary>
        /// 微信支付已成功，给用户充值积分
        /// </summary>
        [Route("saveWeiXinPayOrderInfo")]
        public ActionResult saveWeiXinPayOrderInfo(string userId, string prepayId, int payNum)
        {
            try
            {
                Stream message = Request.Body;
                //存日志
                LogHelper.Log.Error($"微信支付已成功了，并且进入后台开始存储用户积分，用户编号为：{userId}");
                LogHelper.Log.Error($"微信支付已成功了，并且进入后台开始存储用户积分，充值金额为：{payNum}");
                LogHelper.Log.Error($"微信支付已成功了，并且进入后台开始存储用户积分，预下单编号为：{prepayId}");
                //交易成功，给用户充值积分
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
