using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace WxPayAPI
{
    public class Log
    {
  

        /**
         * 向日志写入调试信息
         * @param className 类名
         * @param content 写入内容
         */
        public static void Debug(string className, string content)
        {
            if(WxPayConfig.GetConfig().GetLogLevel() >= 3)
            {
                WriteLog("DEBUG", className, content);
            }
        }

        /**
        * 向日志写入运行时信息
        * @param className 类名
        * @param content 写入内容
        */
        public static void Info(string className, string content)
        {
            if(WxPayConfig.GetConfig().GetLogLevel() >= 2)
            {
                WriteLog("INFO", className, content);
            }
        }

        /**
        * 向日志写入出错信息
        * @param className 类名
        * @param content 写入内容
        */
        public static void Error(string className, string content)
        {
            if(WxPayConfig.GetConfig().GetLogLevel() >= 1)
            {
                WriteLog("ERROR", className, content);
            }
        }

        /**
        * 实际的写日志操作
        * @param type 日志记录类型
        * @param className 类名
        * @param content 写入内容
        */
        protected static void WriteLog(string type, string className, string content)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            //日志内容
            string write_content = time + " " + type + " " + className + ": " + content;
            //需要用户自定义日志实现形式
            Console.WriteLine(write_content);
            writeLog(write_content);

        }
        public static void writeLog(string message)
        {
            string dllpath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            dllpath = dllpath.Substring(8, dllpath.Length - 8);    // 8是 file:// 的长度
            string strFilePath = System.IO.Path.GetDirectoryName(dllpath) + "\\log.txt";
            System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            sw.WriteLine(DateTime.Now.ToString() + "\t" + message);
            sw.Close();
            fs.Close();
        }
    }
}