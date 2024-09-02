using Materal.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    public class RepositoryImpl : IRepository, IScopedDependency<IRepository>
    {
        public void SayHello() => Debug.WriteLine("Hello World!");
    }
}
