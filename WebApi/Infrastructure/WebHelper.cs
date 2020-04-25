using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
   public class WebHelper
    {
        public WebHelper() { }
        public static IHttpContextAccessor Context;
        public WebHelper(IHttpContextAccessor _HttpContext)
        {
            Context = _HttpContext;
        }
        #region Session操作
        /// <summary>
        /// 写Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public void WriteSession<T>(string key, T value)
        {
            if (key.IsEmpty())
                return;
            Context.HttpContext.Session.SetString(key, value.ToString());
        }

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public void WriteSession(string key, string value)
        {
            WriteSession<string>(key, value);
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public string GetSession(string key)
        {
            if (key.IsEmpty())
                return string.Empty;
            return Context.HttpContext.Session.GetString(key);
        }
        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public void RemoveSession(string key)
        {
            if (key.IsEmpty())
                return;
            Context.HttpContext.Session.Remove(key);
        }

        #endregion

        #region Cookie操作
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public void WriteCookie(string strName, string strValue)
        {
            Context.HttpContext.Response.Cookies.Append(strName, strValue);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public void WriteCookie(string strName, string strValue, int expires)
        {
            Context.HttpContext.Response.Cookies.Append(strName, strValue, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(expires)
            });
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public string GetCookie(string strName)
        {
            var Cookies = Context.HttpContext.Request.Cookies[strName];
            if (string.IsNullOrEmpty(Cookies))
                return string.Empty;
            return Cookies;
        }
        /// <summary>
        /// 删除Cookie对象
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        public void RemoveCookie(string CookiesName)
        {
            //HttpCookie objCookie = new HttpCookie(CookiesName.Trim());
            //objCookie.Expires = DateTime.Now.AddYears(-5);
            //HttpContext.Current.Response.Cookies.Add(objCookie);
            Context.HttpContext.Response.Cookies.Delete(CookiesName.Trim());
        }
        #endregion
    }
}
