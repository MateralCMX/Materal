using System;

namespace Materal.RabbitMQHelper.ServerExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = new SimplestProducing();
            service.Run();
            Console.WriteLine("执行完毕");
        }
    }
}
