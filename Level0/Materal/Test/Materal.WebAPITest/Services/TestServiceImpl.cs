using AspectCore.DependencyInjection;
using Materal.WebAPITest.Repository;

namespace Materal.WebAPITest.Services
{
    public class TestServiceImpl : ITestService
    {
        private readonly ITestRepository _testRepository2;
        private ITestRepository? _testRepository;
        [FromServiceContext]
        protected ITestRepository TestRepository
        {
            get => _testRepository ?? throw new InvalidOperationException("未能注入ITestRepository");
            set => _testRepository = value;
        }
        public TestServiceImpl(ITestRepository testRepository2)
        {
            _testRepository2 = testRepository2;
        }
        public string SayHello() => TestRepository.SayHello();
        public async Task<string> SayHelloAsync() => await _testRepository2.SayHelloAsync();
    }
}
