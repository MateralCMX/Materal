using Materal.Extensions.DependencyInjection;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MyInterceptorAttribute : InterceptorAttribute
    {
        public override void After()
        {
            Console.WriteLine("After Invoke");
        }
        public override bool Befor()
        {
            Console.WriteLine("Befor Invoke");
            return true;
        }
    }
}