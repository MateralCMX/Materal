using System;
using System.Threading;

namespace DotNetTestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Console.CancelKeyPress += Console_CancelKeyPress;
            Console.WriteLine("DotNetTestApplication");
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }
            while (true)
            {
                Console.WriteLine("运行中");
                Thread.Sleep(1000);
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("退出按钮");
            e.Cancel = false;
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("退出事件");
            Thread.Sleep(1000);
            Console.WriteLine("退出事件");
        }
    }
}
