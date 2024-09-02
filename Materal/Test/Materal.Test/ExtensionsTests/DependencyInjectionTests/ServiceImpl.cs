using AspectCore.DependencyInjection;
using Materal.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    public class ServiceImpl : IService, IScopedDependency<IService>
    {
#pragma warning disable CS0649
        [FromServiceContext]
        protected IRepository? Repository { get; set; }
#pragma warning restore CS0649
        public void SayHello() => Repository?.SayHello();

        public void Test([Required(ErrorMessage = "消息为空")] string message)
        {
            Debug.WriteLine(message);
        }

        public void Test(TestModel model)
        {
            Debug.WriteLine(model.Message);
        }
    }
}