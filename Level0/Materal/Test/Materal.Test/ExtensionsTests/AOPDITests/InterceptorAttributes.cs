using Materal.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Materal.Test.ExtensionsTests.AOPDITests
{
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
    public class MyInterceptor2Attribute : InterceptorAttribute
    {
        public override void After(InterceptorContext context)
        {
            Debug.WriteLine("After Invoke2");
        }
        public override void Befor(InterceptorContext context)
        {
            Debug.WriteLine("Befor Invoke2");
        }
    }
    public class MyInterceptor3Attribute : InterceptorAttribute
    {
        public override void After(InterceptorContext context)
        {
            Debug.WriteLine("After Invoke3");
        }
        public override void Befor(InterceptorContext context)
        {
            Debug.WriteLine("Befor Invoke3");
        }
    }
}