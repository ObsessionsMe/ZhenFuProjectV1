/*******************************************************************************
 * 
 * 
 * 
 * 
*********************************************************************************/
using log4net;
using log4net.Repository;
using System;
using System.IO;
using System.Web;

namespace Infrastructure.LogConfig
{
    public class LogHelper
    {
        private static ILoggerRepository loggerRepository;
        public static ILoggerRepository LoggerRepository { get; private set; }
        public static ILog Log { get; private set; }

        /// <summary>
        /// 初始化日志
        /// </summary>
        /// <returns></returns>
        public static void LoadLogger()
        {
            LoggerRepository = CreateLoggerRepository();
            LoadLog4NetConfig();
        }

        /// <summary>
        /// 创建日志仓储实例
        /// </summary>
        /// <returns></returns>
        private static ILoggerRepository CreateLoggerRepository()
        {
            loggerRepository = loggerRepository ?? LogManager.CreateRepository("GlobalExceptionHandler"); // 单例
            return loggerRepository;
        }

        /// <summary>
        /// 加载log4net配置
        /// </summary>
        private static void LoadLog4NetConfig()
        {
            // 配置log4net
            log4net.Config.XmlConfigurator.Configure(loggerRepository, new System.IO.FileInfo(Directory.GetCurrentDirectory() + "/Configs/log4net.config"));
            // 创建log实例
            Log = LogManager.GetLogger(loggerRepository.Name, AppDomain.CurrentDomain.FriendlyName);
            Log.Info("已加载日志配置");
        }
    }
}
