using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    [TestClass]
    public class DependencyInjectionTest : MateralTestBase
    {
        public override void AddServices(IServiceCollection services)
        {
            services.TryAddScoped<IService, ServiceImpl>();
            services.TryAddScoped<IRepository, RepositoryImpl>();
        }
        [TestMethod]
        public void PropertyInjectionTest()
        {
            IService service = GetRequiredService<IService>();
            service.SayHello();
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
