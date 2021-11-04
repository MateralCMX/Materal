using System;

namespace DotNetTestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("DotNetTestApplication");
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}
