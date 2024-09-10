using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    [TestClass]
    public class DependencyInjectionTest : MateralTestBase
    {
        [TestMethod]
        public void PropertyInjectionTest()
        {
            IService service = ServiceProvider.GetRequiredService<IService>();
            service.SayHello();
            using IServiceScope serviceScope = ServiceProvider.CreateScope();
            service = serviceScope.ServiceProvider.GetRequiredService<IService>();
            service.SayHello();
            using AsyncServiceScope serviceAsyncScope = ServiceProvider.CreateAsyncScope();
            service = serviceAsyncScope.ServiceProvider.GetRequiredService<IService>();
            service.SayHello();
        }
        [TestMethod]
        public void ValidateInjectionTest()
        {
            IService service = ServiceProvider.GetRequiredService<IService>();
            service.Test(new TestModel { Message = "12345" });
            try
            {
                service.Test(new TestModel { Message = string.Empty });
                Assert.Fail("验证失败");
            }
            catch (ValidationException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            service.Test();
            service.Test("12345");
            try
            {
                service.Test(string.Empty);
                Assert.Fail("验证失败");
            }
            catch (ValidationException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
