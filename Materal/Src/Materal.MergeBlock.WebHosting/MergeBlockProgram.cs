using Materal.MergeBlock.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Materal.MergeBlock.WebHosting
{
    /// <summary>
    /// MergeBlock程序
    /// </summary>
    public static class MergeBlockProgram
    {
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task RunAsync(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
#if NET8_0_OR_GREATER
            builder.AddMergeBlockCore();
#else
            builder.Services.AddMergeBlockCore(builder.Configuration);
#endif
            WebApplication app = builder.Build();
            app.UseMergeBlock();
            app.MapGet("/Hello", (HttpContext httpContext) => "Hello World!");
            await app.RunAsync();
        }
    }
}
