using System;

namespace TestWebSocket.Common
{
    public class ConsoleHelper
    {
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
        /// 测试控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void TestWriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteLine("Test", message, subTitle, consoleColor);
        }
    }
}
