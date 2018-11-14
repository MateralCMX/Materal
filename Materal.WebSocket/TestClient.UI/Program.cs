using System;
using Microsoft.Extensions.DependencyInjection;
using TestClient.Common;

namespace TestClient.UI
{
    internal class Program
    {
        /// <summary>
        /// 数据解析服务
        /// </summary>
        private static ITestClient _testClient;
        /// <summary>
        /// 程序入口
        /// </summary>
        public static void Main()
        {
            TestClientHelper.RegisterCustomerService();
            TestClientHelper.Services.AddSingleton<ITestClient, TestClientImpl>();
            TestClientHelper.BulidService();
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
                            ConsoleHelper.TestClientWriteLine("Exit 或 按下Ctrl + C | 退出程序");
                            ConsoleHelper.TestClientWriteLine("Reload               | 重启服务");
                            ConsoleHelper.TestClientWriteLine("Help                 | 退出");
                            break;
                        case "Reload":
                            ConsoleHelper.TestClientWriteLine("正在重启服务");
                            StopTestClient();
                            StartTestClient();
                            break;
                        default:
                            ConsoleHelper.TestClientWriteLine("未识别命令，输入Help查看命令");
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
                ConsoleHelper.TestClientWriteLine("发生未知错误：" + ex.Message);
                ExitApplication();
            }
        }
        /// <summary>
        /// 停止数据解析服务
        /// </summary>
        private static void StopTestClient(bool writeMessage = true)
        {
            if (writeMessage)
            {
                ConsoleHelper.TestClientWriteLine("正在关闭服务");
            }
            _testClient?.Dispose();
        }
        /// <summary>
        /// 开始数据解析服务
        /// </summary>
        private static void StartTestClient()
        {
            ConsoleHelper.TestClientWriteLine("正在启动服务");
            _testClient = TestClientHelper.ServiceProvider.GetService<ITestClient>();
            _testClient.IsAutoReload = true;
            _testClient.Init();
            _testClient.Start();
        }
        /// <summary>
        /// 关闭应用程序
        /// </summary>
        private static void CloseApplication()
        {
            _testClient.IsAutoReload = false;
            StopTestClient();
            ExitApplication();
        }
        /// <summary>
        /// 退出应用程序
        /// </summary>
        private static void ExitApplication()
        {
            ConsoleHelper.TestClientWriteLine("按任意键退出......");
            Console.ReadKey();
        }
    }
}
