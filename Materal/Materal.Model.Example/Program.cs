using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Materal.Model.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection service = new ServiceCollection();
            service.AddTransient<ITestService, TestServiceImpl>();
            ServiceProvider serviceProvider = service.BuildServiceProvider();
            var testService = serviceProvider.GetService<ITestService>();
            testService.Test01(new TempModel
            {
                ID = Guid.Empty
            });
            var example = new ExampleByFilterModel();
            example.Example();
            Console.ReadKey();
        }
    }
}
