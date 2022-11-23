using System;

namespace Materal.ConvertHelper.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var example = new ExampleByObjectExtension();
            ExampleByObjectExtension.ConvertToExample1();
            Console.ReadKey();
        }
    }
}
