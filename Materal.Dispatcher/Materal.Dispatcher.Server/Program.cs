using Materal.Dispatcher.Common;
using System;

namespace Materal.Dispatcher.Server
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                Console.Title = "DispatcherServer";
                var dispatcherServer = ServerHelper.GetService<IDispatcherServer>();
                dispatcherServer.Start().Wait();
                ConsoleHelper.TestWriteLine("DispatcherServer已启动，输入Exit退出。");
                while (Console.ReadLine() != "Exit"){}
                dispatcherServer.Stop().Wait();
            }
            catch (Exception ex)
            {
                ConsoleHelper.TestWriteLine(ex.Message, "Error", ConsoleColor.Red);
            }
            ConsoleHelper.TestWriteLine("DispatcherServer已停止，按任意键退出。");
            Console.ReadKey();
        }
    }
}
