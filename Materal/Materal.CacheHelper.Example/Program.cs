using System;

namespace Materal.CacheHelper.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var example = new ExampleByICacheManager();
            example.SetBySlidingExample1();
            Console.ReadKey();
        }
    }
}
