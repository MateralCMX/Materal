using Materal.NetworkHelper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            ClassA.TempList.Add("1123");
            var a = new ClassA();
            ClassA.TempList.Add("1123");
            var b = new ClassA();
            ClassA.TempList.Add("1123");
            Console.ReadKey();
        }
    }

    public class ClassA
    {
        public static List<string> TempList = new List<string>();
    }
}
