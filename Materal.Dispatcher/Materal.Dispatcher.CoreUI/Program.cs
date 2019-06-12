using Materal.Dispatcher.Common;
using Materal.Dispatcher.Server;
using System;

namespace Materal.Dispatcher.CoreUI
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
                ConsoleHelper.ServerWriteLine("DispatcherServer已启动，输入Exit退出。");
                while (Console.ReadLine() != "Exit"){}
                dispatcherServer.Stop().Wait();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ServerWriteLine(ex.Message, "Error", ConsoleColor.Red);
            }
            ConsoleHelper.ServerWriteLine("DispatcherServer已停止，按任意键退出。");
            Console.ReadKey();
        }
    }
}
