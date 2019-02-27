using Microsoft.Extensions.DependencyInjection;
using System;
using TestWebSocket.Common;

namespace TestClient.UI
{
    internal class Program
    {
        /// <summary>
        /// 测试客户端
        /// </summary>
        private static ITestClient _testClient;
        /// <summary>
        /// 程序入口
        /// </summary>
        public static void Main()
        {
            TestClientHelper.RegisterCustomerService();
            TestClientHelper.Services.AddSingleton<ITestClient, TestClientImpl>();
            TestClientHelper.BuildService();
            try
            {
                StopTestClient(false);
                StartTestClient();
                var isExit = false;
                do
                {
                    var readStr = Console.ReadLine();
                    switch (readStr)
                    {
                        case "Exit":
                            isExit = true;
                            break;
                        case "Help":
                            ConsoleHelper.TestWriteLine("Exit 或 按下Ctrl + C | 退出程序");
                            ConsoleHelper.TestWriteLine("Reload               | 重启服务");
                            ConsoleHelper.TestWriteLine("Help                 | 退出");
                            break;
                        case "Reload":
                            ConsoleHelper.TestWriteLine("正在重启服务");
                            StopTestClient();
                            StartTestClient();
                            break;
                        default:
                            _testClient.SendMessage(readStr);
                            break;
                    }
                    if (isExit)
                    {
                        break;
                    }
                } while (true);
                CloseApplication();
            }
            catch (Exception ex)
            {
                ConsoleHelper.TestWriteLine("发生未知错误：" + ex.Message);
                ExitApplication();
            }
        }
        /// <summary>
        /// 停止测试客户端
        /// </summary>
        private static void StopTestClient(bool writeMessage = true)
        {
            if (writeMessage)
            {
                ConsoleHelper.TestWriteLine("正在关闭服务");
            }
            _testClient?.Stop();
        }
        /// <summary>
        /// 开始测试客户端
        /// </summary>
        private static void StartTestClient()
        {
            ConsoleHelper.TestWriteLine("正在启动服务");
            _testClient = TestClientHelper.ServiceProvider.GetService<ITestClient>();
            _testClient.Init();
            _testClient.Start();
        }
        /// <summary>
        /// 关闭应用程序
        /// </summary>
        private static void CloseApplication()
        {
            StopTestClient();
            ExitApplication();
        }
        /// <summary>
        /// 退出应用程序
        /// </summary>
        private static void ExitApplication()
        {
            ConsoleHelper.TestWriteLine("按任意键退出......");
            Console.ReadKey();
        }
    }
}
