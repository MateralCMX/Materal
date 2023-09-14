using Microsoft.Extensions.DependencyInjection;
using Materal.Extensions.DependencyInjection;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
    [TestClass]
    public class AOPDITest : BaseTest
    {
        [TestMethod]
        public void DITest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IService, ServiceImpl>();
            IServiceProvider serviceProvider = services.BuildMateralServiceProvider();
            IService aopService = serviceProvider.GetRequiredService<IService>();

            aopService.SayHello();
            aopService.SayHello("Materal");
            //string name = aopService.GetName();
            //Console.WriteLine($"name={name}");
            //name = aopService.GetName("宝盖");
            //Console.WriteLine($"name={name}");
        }
    }
}
