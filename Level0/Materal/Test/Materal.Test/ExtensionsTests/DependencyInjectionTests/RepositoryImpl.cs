namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    public class RepositoryImpl : IRepository
    {
        public void SayHello() => Console.WriteLine("Hello World!");
    }
}
