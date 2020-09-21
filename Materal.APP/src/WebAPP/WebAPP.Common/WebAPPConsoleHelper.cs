using System;
using Materal.APP.Core;

namespace WebAPP.Common
{
    public static class WebAPPConsoleHelper
    {
        private const string _appName = "WebAPP";
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void Write(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleHelperBase.Write(_appName, message, subTitle, consoleColor);
        }
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void WriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleHelperBase.WriteLine(_appName, message, subTitle, consoleColor);
        }
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="exception">消息</param>
        public static void WriteLine(Exception exception)
        {
            string message = ConsoleHelperBase.GetMessage(exception);
            ConsoleHelperBase.WriteLine(_appName, message, "Error", ConsoleColor.Red);
        }
    }
}