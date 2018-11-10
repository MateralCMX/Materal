using NLog;
using System;

namespace NLogTest.UI
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetLogger("File");
        public static void Main()
        {
            Console.WriteLine("执行开始");
            Logger.Error("Hello World");
            Console.WriteLine("执行结束");
            Console.ReadKey();
        }
    }
}
