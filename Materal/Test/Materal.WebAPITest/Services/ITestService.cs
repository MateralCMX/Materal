namespace Materal.WebAPITest.Services
{
    public interface ITestService
    {
        string SayHello();
        Task<string> SayHelloAsync();
        string SayHello(string name);
    }
}
