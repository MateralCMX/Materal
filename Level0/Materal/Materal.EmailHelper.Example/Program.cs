using System;

namespace Materal.EmailHelper.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var example = new ExampleByEmailManager();
            example.SendMail();
            Console.ReadKey();
        }
    }
}
