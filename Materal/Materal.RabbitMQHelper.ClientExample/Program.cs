using System;

namespace Materal.RabbitMQHelper.ClientExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = new SimplestConsuming();
            //service.Run();
            service.RunAsync();
            Console.WriteLine("执行完毕");
        }
    }
}
