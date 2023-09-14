using Materal.Extensions.DependencyInjection;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
    public class TestServiceImpl : IService
    {
        private readonly ServiceImpl _service;
        public TestServiceImpl(ServiceImpl service) => _service = service;
        //public string GetName() => InterceptorHelper.Handler<IService, string>(nameof(GetName), () => _service.GetName());
        //public string GetName(string name) => InterceptorHelper.Handler<IService, string>(nameof(GetName), () => _service.GetName(name), name);
        public void SayHello() => InterceptorHelper.Handler<IService>(nameof(SayHello), _service);
        public void SayHello(string name) => InterceptorHelper.Handler<IService>(nameof(SayHello), _service, name);
    }
}