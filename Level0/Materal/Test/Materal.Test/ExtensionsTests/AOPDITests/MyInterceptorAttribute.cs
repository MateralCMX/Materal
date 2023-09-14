using Materal.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MyInterceptorAttribute : InterceptorAttribute
    {
        public override void After(InterceptorContext context)
        {
            Debug.WriteLine("After Invoke");
        }
        public override void Befor(InterceptorContext context)
        {
            Debug.WriteLine("Befor Invoke");
        }
    }
}