using AspectCore.DependencyInjection;
using Materal.WebAPITest.Repository;

namespace Materal.WebAPITest.Services
{
    public class TestServiceImpl(ITestRepository testRepository2) : ITestService
    {
        private ITestRepository? _testRepository;
        [FromServiceContext]
        protected ITestRepository TestRepository
        {
            get => _testRepository ?? throw new InvalidOperationException("未能注入ITestRepository");
            set => _testRepository = value;
        }

        public string SayHello() => TestRepository.SayHello();
        public async Task<string> SayHelloAsync() => await testRepository2.SayHelloAsync();
        public string SayHello(string name) => $"Hello {name}!";
    }
}
