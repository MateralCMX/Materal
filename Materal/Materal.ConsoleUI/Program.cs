using Materal.NetworkHelper;
using Materal.WindowsHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Materal.Common;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static async Task Main()
        {
            var aModel = new
            {
                Name = "Materal",
                Name2 = "Materal"
            };
            var bModel = new
            {
                Name = "Materal",
                Name2 = "Materal",
                Name3 = "Materal"
            };
            bool result = aModel.PropertyContain(bModel, new Dictionary<string, Func<bool>>
            {
                [nameof(aModel.Name2)] = () => aModel.Name2 == bModel.Name2 && aModel.Name2 != bModel.Name3
            });
            Console.WriteLine(result);
        }
    }
}
