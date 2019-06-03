using System;
using System.Threading.Tasks;
using TestWebSocket.Common;

namespace TestServer.UI
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                Console.Title = "WebSocketServer";
                var socketServer = TestServerHelper.GetService<ITestServer>();
                Task.WaitAll(socketServer.RunServerAsync());
            }
            catch (Exception ex)
            {
                ConsoleHelper.TestWriteLine(ex.Message, "Error", ConsoleColor.Red);
            }
            ConsoleHelper.TestWriteLine("TestServer服务器已停止，按任意键退出。");
            Console.ReadKey();
        }
    }
}
