using DemoCore;
using System;
using System.Threading.Tasks;

namespace ConsoleDemo_DotNetCore3_1
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine(await ConfigCenterClientDemo.GetConfigAsync());
            Console.ReadKey();
        }
    }
}
