namespace Materal.Test.ExtensionsTests.AOPDITests
{
    public interface IService
    {
        [MyInterceptor]
        void SayHello();
        [MyInterceptor]
        void SayHello(string name);
        //[MyInterceptor]
        //string GetName();
        //[MyInterceptor]
        //string GetName(string name);
    }
}