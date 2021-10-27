using Materal.APP.Core;
using System;

namespace Deploy.Common
{
    public static class DeployConsoleHelper
    {
        private const string AppName = "Deploy";
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void Write(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleHelperBase.Write(AppName, message, subTitle, consoleColor);
        }
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        /// <param name="consoleColor">颜色</param>
        public static void WriteLine(string message, string subTitle = null, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleHelperBase.WriteLine(AppName, message, subTitle, consoleColor);
        }
        /// <summary>
        /// 控制台输出Debug
        /// </summary>
        /// <param name="message"></param>
        public static void WriteDebug(string message)
        {
            WriteLine(message, "Debug", ConsoleColor.DarkGreen);
        }
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="exception">消息</param>
        public static void WriteLine(Exception exception)
        {
            string message = ConsoleHelperBase.GetMessage(exception);
            ConsoleHelperBase.WriteLine(AppName, message, "Error", ConsoleColor.Red);
        }
    }
}
