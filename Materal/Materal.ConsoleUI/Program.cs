using System;
using Materal.StringHelper;

namespace Materal.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string inputStr = Console.ReadLine();
                Console.WriteLine(inputStr.IsNumber());
            }
        }
    }
}
