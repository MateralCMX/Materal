using System;
using System.Reflection;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Model.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //IServiceCollection service = new ServiceCollection();
            //service.AddTransient<ITestService, TestServiceImpl>();
            //ServiceProvider serviceProvider = service.BuildDynamicProxyServiceProvider();
            //var testService = serviceProvider.GetService<ITestService>();
            //testService.Test01(null);
            var example = new ExampleByFilterModel();
            example.Example();
            Console.ReadKey();
        }
    }
}
