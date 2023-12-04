using Materal.Extensions.DependencyInjection;
using Materal.Utils;

namespace Materal.WebAPITest
{
    public class MyInterceptorAttribute : InterceptorAttribute
    {
        public override void Befor(InterceptorContext context)
        {
            ConsoleQueue.WriteLine("Befor", ConsoleColor.DarkGreen);
        }
        public override void After(InterceptorContext context)
        {
            ConsoleQueue.WriteLine("After", ConsoleColor.DarkGreen);
        }
    }
}