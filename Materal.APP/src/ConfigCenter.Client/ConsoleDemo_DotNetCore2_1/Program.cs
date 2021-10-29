using System;
using System.Threading.Tasks;
using DemoCore;

namespace ConsoleDemo_DotNetCore2_1
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
