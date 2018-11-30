using System;
using System.Text.RegularExpressions;
using Materal.ConvertHelper;
using Materal.StringHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            const string inputStr = "2007-02-29 06:14:44";

            bool result = inputStr.IsDateTime();
            Console.ReadKey();
        }
    }
}
