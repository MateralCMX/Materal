using Materal.MergeBlock.Extensions;

namespace Materal.MergeBlock.AspNetTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddMergeBlockCore();
            WebApplication app = builder.Build();
            app.UseMergeBlock();
            app.MapGet("/Hello", (HttpContext httpContext) => "Hello World!");
            app.Run();
        }
    }
}
