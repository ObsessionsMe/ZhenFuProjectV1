using log4net.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.LogConfig
{
    /// <summary>
    ///  全局异常处理
    /// </summary>
    public class GlobleExceptionAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            LogHelper.Log.Error(context.Exception);
        }
    }
}
