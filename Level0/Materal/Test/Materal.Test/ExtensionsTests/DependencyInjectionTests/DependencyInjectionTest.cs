namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    [TestClass]
    public class DependencyInjectionTest : BaseTest
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
        }
    }
}
