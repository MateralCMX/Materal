namespace Materal.WebAPITest.Services
{
    public interface ITestService
    {
        string SayHello();
        Task<string> SayHelloAsync();
    }
    public class TestServiceImpl : ITestService
    {
        public string SayHello() => "Hello World!";

        public Task<string> SayHelloAsync() => Task.FromResult("Hello World!");
    }
}
