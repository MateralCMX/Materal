using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    public class RepositoryImpl : IRepository
    {
        public void SayHello() => Debug.WriteLine("Hello World!");
    }
}
