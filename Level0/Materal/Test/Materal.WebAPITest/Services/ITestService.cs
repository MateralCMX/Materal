using Materal.Extensions.DependencyInjection;

namespace Materal.WebAPITest.Services
{
    public interface ITestService
    {
        string SayHello();
        Task<string> SayHelloAsync();
    }
    public class TestServiceImpl : ITestService
    {
        private ITestRepository? _testRepository;
        [PropertyInjection]
        protected ITestRepository TestRepository
        {
            get => _testRepository ?? throw new InvalidOperationException("未能注入ITestRepository");
            set => _testRepository = value;
        }
        public string SayHello() => TestRepository.SayHello();
        public async Task<string> SayHelloAsync() => await TestRepository.SayHelloAsync();
    }
    public interface ITestRepository
    {
        string SayHello();
        Task<string> SayHelloAsync();
    }
    public class TestRepositoryImpl : ITestRepository
    {
        public string SayHello() => "Hello World!";

        public Task<string> SayHelloAsync() => Task.FromResult("Hello World!");
    }
}
