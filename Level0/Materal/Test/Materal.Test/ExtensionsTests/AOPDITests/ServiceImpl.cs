using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
    public class ServiceImpl : IService
    {
        //public string GetName() => "Materal";

        //public string GetName(string name) => name;

        public void SayHello() => Debug.WriteLine("Hello World!");

        public void SayHello(string name) => Debug.WriteLine($"Hello {name}!");
    }
}