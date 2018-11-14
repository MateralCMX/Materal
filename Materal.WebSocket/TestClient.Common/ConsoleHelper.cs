using System;
using MateralTools.MVerify;

namespace TestClient.Common
{
    public class ConsoleHelper
    {
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        public static void WriteLine(string title, string message, string subTitle)
        {
            Console.WriteLine(subTitle.MIsNullOrEmpty() ? $"{title}：{message}" : $"{title}[{subTitle}]：{message}");
        }
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="subTitle">副标题</param>
        public static void TestClientWriteLine(string message, string subTitle = null)
        {
            WriteLine("TestClient", message, subTitle);
        }
    }
}
