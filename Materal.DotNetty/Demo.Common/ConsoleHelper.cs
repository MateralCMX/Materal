using System;

namespace Demo.Common
{
    public class ConsoleHelper
    {
        private const string ServerName = "DemoServer";
        /// <summary>
        /// 写入控制台
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void Write(string title, string message, string subTitle, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            string dateNowTxt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Console.Write(string.IsNullOrEmpty(subTitle)?$"[{dateNowTxt}]{title}:{message}" : $"[{dateNowTxt}]{title}[{subTitle}]:{message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// 写入控制台
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void WriteLine(string title, string message, string subTitle, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            string dateNowTxt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Console.WriteLine(string.IsNullOrEmpty(subTitle) ? $"[{dateNowTxt}]{title}:{message}" : $"[{dateNowTxt}]{title}[{subTitle}]:{message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// 写入控制台
        /// </summary>
        /// <param name="message"></param>
        /// <param name="subTitle"></param>
        /// <param name="consoleColor"></param>
        public static void ServerWrite(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Write(ServerName, message, subTitle, consoleColor);
        }
        /// <summary>
        /// 写入控制台
        /// </summary>
        /// <param name="message"></param>
        /// <param name="subTitle"></param>
        /// <param name="consoleColor"></param>
        public static void ServerWriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteLine(ServerName, message, subTitle, consoleColor);
        }
        /// <summary>
        /// 写入错误
        /// </summary>
        /// <param name="exception"></param>
        public static void ServerWriteError(Exception exception)
        {
            ServerWriteLine(GetErrorMessage(exception), "错误", ConsoleColor.Red);
        }
        #region 私有方法
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static string GetErrorMessage(Exception exception)
        {
            string message = $"{exception.Message}";
            if (!string.IsNullOrEmpty(exception.StackTrace)) message += $"\r\n{exception.StackTrace}";
            if(exception is AggregateException aggregateException)
            {
                foreach (Exception innerException in aggregateException.InnerExceptions)
                {
                    Exception ex;
                    do
                    {
                        message += $"\r\n{GetErrorMessage(exception)}";
                        ex = innerException.InnerException;
                    } while (ex != null);
                }
            }
            else
            {
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    message += $"\r\n{GetErrorMessage(exception)}";
                }
            }
            return message;
        }
        #endregion
    }
}
