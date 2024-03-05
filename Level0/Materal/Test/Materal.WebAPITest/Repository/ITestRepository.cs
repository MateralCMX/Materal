namespace Materal.WebAPITest.Repository
{
    public interface ITestRepository
    {
        string SayHello();
        Task<string> SayHelloAsync();
    }
}
