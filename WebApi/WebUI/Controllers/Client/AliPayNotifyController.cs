using System;
using System.Collections.Generic;
using System.Linq;
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
using NPOI.SS.Formula.Functions;
using RepositoryFactory.ServiceInterface;
using UtilitieEntity;
using WebUI.App_Start;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  商品模块：首页商品展示/商品详情
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AliPayNotifyController : Controller
    {
        private readonly IUserRepository userRepository;
        public AliPayNotifyController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AsyAliPayNotifyInfo")]
        public void AsyAliPayNotifyInfo()
        {
            try
            {
                LogHelper.Log.Error($"AsyAliPayNotifyInfo-Quray:进来了");
                //var entity = new AliNotify_Entity();
                string orderStatus = Request.Form["trade_status"];
                if (string.IsNullOrEmpty(orderStatus))
                {
                    LogHelper.Log.Error($"AsyAliPayNotifyInfo-订单状态为空:{orderStatus}");
                    return;
                }
                if (orderStatus != "TRADE_SUCCESS")
                {
                    LogHelper.Log.Error($"AsyAliPayNotifyInfo-订单未付款成功:{orderStatus}");
                    return;
                }
                LogHelper.Log.Error($"AsyAliPayNotifyInfo-UserId:{Request.Form["passback_params"]}");
                LogHelper.Log.Error($"AsyAliPayNotifyInfo-receipt_amount:{Request.Form["receipt_amount"]}");
                string UserId = Request.Form["passback_params"];
                string receipt_amount = Request.Form["receipt_amount"];
                if (orderStatus == "TRADE_SUCCESS")
                {
                    //交易成功，给用户充值积分
                    UserId = HttpUtility.UrlDecode(UserId);
                    UserManageService service = new UserManageService(userRepository);
                    decimal payNum = Convert.ToDecimal(receipt_amount);
                    var data = service.PayPorints(payNum, UserId);
                    if (data == null)
                    {
                        LogHelper.Log.Error($"AsyAliPayNotifyInfo-积分保存失败:{data}");
                    }
                }
                //entity.trade_status = Request.Form["trade_status"];            
                //entity.trade_no = Request.Form["trade_no"];
                //entity.out_trade_no = Request.Form["out_trade_no"];//商户订单号，由商户传入的
                //entity.sign = Request.Form["sign"];
                //entity.receipt_amount = Request.Form["receipt_amount"];
                //entity.notify_time = Request.Form["notify_time"];
                //entity.gmt_close = Request.Form["gmt_close"];

            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"AsyAliPayNotifyInfo-Exception:{ex}");
                throw ex;
            }
        }


        [Route("AliPayPorints")]
        public ActionResult AliPayPorints(string userId, float payNum)
        {
            if (payNum <= 0)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "充值金额有误，请重新输入", data = "" });
            }
            DefaultAopClient client = new DefaultAopClient(AliPayParams.gatewayUrl, AliPayParams.app_id, AliPayParams.private_key, "json", "1.0", AliPayParams.sign_type, AliPayParams.alipay_public_key, AliPayParams.charset, false);

            // 外部订单号，商户网站订单系统中唯一的订单号
            string out_trade_no = Common.CreateNo();

            // 订单名称/商品名称
            string subject = "积分充值";

            // 付款金额
            string total_amout = payNum.ToString();

            // 商品描述
            string body = "充值后可获得积分，在商城中兑换商品";

            // 支付中途退出返回商户网站地址
            string quit_url = AliPayParams.SetReturnUrl;

            // 组装业务参数model
            AlipayTradeWapPayModel model = new AlipayTradeWapPayModel();
            model.Body = body;
            model.Subject = subject;
            model.TotalAmount = total_amout;
            model.OutTradeNo = out_trade_no;
            model.ProductCode = "QUICK_WAP_WAY";
            model.QuitUrl = quit_url;
            model.PassbackParams = HttpUtility.UrlEncode(userId);
            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
            // 设置支付完成同步回调地址
            request.SetReturnUrl(AliPayParams.SetReturnUrl);
            // 设置支付完成异步通知接收地址
            request.SetNotifyUrl(AliPayParams.SetNotifyUrl);
            //将业务model载入到request
            request.SetBizModel(model);

            AlipayTradeWapPayResponse response = null;
            try
            {
                response = client.pageExecute(request, null, "post");
                LogHelper.Log.Debug($"response.Body:{response.Body}");
                //Response.WriteAsync(response.Body);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "下单成功", data = response.Body });
        }
    }
}
