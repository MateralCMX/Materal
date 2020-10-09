using Materal.Common;
using System;
using System.Collections.Generic;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
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
