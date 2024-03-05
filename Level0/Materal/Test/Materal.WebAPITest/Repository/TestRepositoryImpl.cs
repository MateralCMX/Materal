namespace Materal.WebAPITest.Repository
{
    public class TestRepositoryImpl : ITestRepository
    {
        public string SayHello() => "Hello World!";

        public Task<string> SayHelloAsync() => Task.FromResult("Hello World!");
    }
}
