using Materal.ConfigurationHelper;
using Materal.ConsoleUI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        static void Main()
        {
            List<ConfigurationHelperArrayModel> result = ApplicationConfig.Configuration.GetArrayObjectValue<ConfigurationHelperArrayModel>("ConfigurationHelper:Array");
            //Task.Run(async () => { await Init(); });
            Console.ReadKey();
        }

        public static async Task Init()
        {
        }
    }
}
