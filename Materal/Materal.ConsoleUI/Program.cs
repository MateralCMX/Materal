using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            const int count = 100000000;
            var intArray = new int[count];
            var intList = new List<int>();
            for (var i = 0; i < count; i++)
            {
                intArray[i] = i;
                intList.Add(i);
            }

            var temp = 0;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (int item in intArray)
            {
                temp = item;
            }
            stopWatch.Stop();
            Console.WriteLine("Array:" + stopWatch.Elapsed);
            stopWatch.Restart();
            foreach (int item in intList)
            {
                temp = item;
            }
            stopWatch.Stop();
            Console.WriteLine("List:" + stopWatch.Elapsed);
            Console.ReadKey();
        }
    }
}
