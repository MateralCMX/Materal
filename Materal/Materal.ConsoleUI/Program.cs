using Materal.ConfigurationHelper;
using System;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        static void Main()
        {
            string config = ApplicationConfig.Configuration["ConfigurationHelper:Set"];
            Console.WriteLine($"当前值：{config}");
            while (true)
            {
                config = Console.ReadLine();
                ApplicationConfig.Configuration.SetValue("ConfigurationHelper:Set", config);
                config = ApplicationConfig.Configuration["ConfigurationHelper:Set"];
                Console.WriteLine($"当前值：{config}");
            }
        }
    }
}
