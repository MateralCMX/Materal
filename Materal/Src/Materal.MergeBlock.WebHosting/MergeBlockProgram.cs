using Materal.Extensions;
using Materal.Extensions.DependencyInjection;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

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
            builder.Host.UseMateralServiceProvider();
            builder.Services.AddSingleton(builder);
            IMvcBuilder mvcBuilder = builder.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = true;
            });
            builder.Services.AddSingleton(mvcBuilder);
#if NET8_0_OR_GREATER
            builder.Services.AddSingleton<IHostApplicationBuilder>(builder);
            builder.AddMergeBlockCore();
#else
            builder.Services.AddMergeBlockCore(builder.Configuration);
#endif
            mvcBuilder.AddApplicationPart(typeof(MergeBlockProgram).Assembly);
            MergeBlockContext? mergeBlockContext = builder.Services.GetSingletonInstance<MergeBlockContext>();
            if (mergeBlockContext is not null)
            {
                foreach (Assembly assembly in mergeBlockContext.MergeBlockAssemblies)
                {
                    mvcBuilder.AddApplicationPart(assembly);
                }
            }
            builder.Services.AddEndpointsApiExplorer();//添加API探索器
            WebApplication app = builder.Build();
            app.Use(async (context, next) => // 使请求可重复读取
            {
                context.Request.EnableBuffering();
                await next.Invoke(context);
            });
            app.UseMergeBlock();
            app.MapControllers();
            AutoUseHttpsRedirection(app);
            await app.RunAsync();
        }
        /// <summary>
        /// 自动使用Https重定向
        /// </summary>
        /// <param name="app"></param>
        private static void AutoUseHttpsRedirection(WebApplication app)
        {
            List<Uri> urlsList = []; //URLS
            IConfiguration configuration = app.Services.GetRequiredService<IConfiguration>();
            string? urlsValue = configuration.GetConfigItemToString("URLS");
            if (string.IsNullOrWhiteSpace(urlsValue) && app.Environment.IsDevelopment())
            {
                urlsValue ??= configuration.GetConfigItemToString("ASPNETCORE_URLS");
            }
            urlsValue ??= "http://localhost:5000";
            string[] urls = urlsValue.Split(";");
            foreach (string url in urls)
            {
                urlsList.Add(new(url));
            }
            #region 处理Url相关
            if (urlsList.Any(m => m.Scheme == "https"))
            {
                app.UseHttpsRedirection(); //启用HTTPS重定向
            }
            #endregion
        }
    }
}
