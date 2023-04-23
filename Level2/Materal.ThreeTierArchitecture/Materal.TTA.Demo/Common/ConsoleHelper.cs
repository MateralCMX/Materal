using System;

namespace Common
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
        public static void WebSocketServerWriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteLine("WebSocketServer", message, subTitle, consoleColor);
        }
        /// <summary>
        /// WebSocket服务器控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void WebSocketServerWrite(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Write("WebSocketServer", message, subTitle, consoleColor);
        }
        /// <summary>
        /// Log服务器控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void LogServerWriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteLine("LogServer", message, subTitle, consoleColor);
        }
        /// <summary>
        /// Log服务器控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void LogServerWrite(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Write("LogServer", message, subTitle, consoleColor);
        }
        /// <summary>
        /// Bootstrap控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void BootstrapWriteLine(string message, string subTitle = "", ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteLine("Bootstrap", message, subTitle, consoleColor);
        }
        /// <summary>
        /// Bootstrap控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void BootstrapWrite(string message, string subTitle = "", ConsoleColor consoleColor = ConsoleColor.White)
        {
            Write("Bootstrap", message, subTitle, consoleColor);
        }

        /// <summary>
        /// 敏感词服务器控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void SensitiveWordServerWriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteLine("SensitiveWordServer", message, subTitle, consoleColor);
        }
        /// <summary>
        /// 敏感词服务器控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void SensitiveWordServerWrite(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Write("SensitiveWordServer", message, subTitle, consoleColor);
        }
    }
}
