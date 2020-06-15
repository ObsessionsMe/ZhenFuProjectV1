using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using Infrastructure.LogConfig;

namespace WxPayAPI
{
    /// <summary>
    /// 回调处理基类
    /// 主要负责接收微信支付后台发送过来的数据，对数据进行签名验证
    /// 子类在此类基础上进行派生并重写自己的回调处理过程
    /// </summary>
    public class Notify
    {
        /// <summary>
        /// 接收从微信支付后台发送过来的数据并验证签名
        /// </summary>
        /// <returns>微信支付后台返回的数据</returns>
        public WxPayData GetNotifyData(StringBuilder stringBuilder)
        {
            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(stringBuilder.ToString());
            }
            catch(WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                //Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                LogHelper.Log.Error($"签名校验失败，微信支付失败：{res.ToXml()}");
                //page.Response.Write(res.ToXml());
                //page.Response.End();
            }
            //Log.Info(this.GetType().ToString(), "Check sign success");
            LogHelper.Log.Error($"签名校成功：{data.FromXml(stringBuilder.ToString())}");
            return data;
        }
    }
}