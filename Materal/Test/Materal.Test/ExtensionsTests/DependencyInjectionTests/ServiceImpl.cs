using Materal.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    public class ServiceImpl : IService, IScopedDependency<IService>
    {
        private IRepository? _repository;
        [FromServices]
        protected IRepository Repository { get => _repository ?? throw new ArgumentNullException("获取仓储失败"); set => _repository = value; }
        public void SayHello() => Repository.SayHello();
        public void Test([Required(ErrorMessage = "消息为空")] string message) => Debug.WriteLine(message);
        public void Test(TestModel model) => Debug.WriteLine(model.Message);
    }
}