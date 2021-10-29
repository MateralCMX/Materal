using DemoCore;
using System;
using System.Threading.Tasks;

namespace ConsoleDemo_DotNet5
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