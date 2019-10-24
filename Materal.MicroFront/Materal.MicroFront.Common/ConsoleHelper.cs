using System;

namespace Materal.MicroFront.Common
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void Write(string title, string message, string subTitle, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            string dateNow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Console.Write(string.IsNullOrEmpty(subTitle) ? $"[{dateNow}]{title}：{message}" : $"[{dateNow}]{title}[{subTitle}]：{message}");
        }
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void WriteLine(string title, string message, string subTitle, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            string dateNow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Console.WriteLine(string.IsNullOrEmpty(subTitle) ? $"[{dateNow}]{title}：{message}" : $"[{dateNow}]{title}[{subTitle}]：{message}");
        }
        /// <summary>
        /// WebSocket服务器控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void MicroFrontServerWriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteLine("MicroFrontServer", message, subTitle, consoleColor);
        }
        /// <summary>
        /// WebSocket服务器控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void MicroFrontServerWrite(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Write("MicroFrontServer", message, subTitle, consoleColor);
        }
        /// <summary>
        /// WebSocket服务器控制台输出错误
        /// </summary>
        /// <param name="exception">错误</param>
        public static void MicroFrontServerErrorWriteLine(Exception exception)
        {
            string message = $"{exception.Message}\r\n{exception.StackTrace}\r\n";
            if (exception is AggregateException aggregateException)
            {
                foreach (Exception ex in aggregateException.InnerExceptions)
                {
                    Exception innerException = ex;
                    do
                    {
                        message += $"{innerException.Message}\r\n{innerException.StackTrace}\r\n";
                        innerException = innerException.InnerException;
                    } while (innerException != null);
                }
            }
            else
            {
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    message += $"{exception.Message}\r\n{exception.StackTrace}\r\n";
                }
            }
            MicroFrontServerWriteLine(message, null, ConsoleColor.Red);
        }
    }
}
