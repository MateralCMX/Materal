using Materal.Common;
using Materal.TTA.EFRepository;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using RC.Core.WebAPI;
using RC.Core.WebAPI.Common;
using RC.Deploy.Common;
using RC.Deploy.RepositoryImpl;
using RC.Deploy.Services;

namespace RC.Deploy.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program : BaseProgram
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplication app = Start(args, services =>
            {
                services.AddDeployService();
            }, "RC.Deploy");
            #region 处理重写
            RewriteOptions rewriteOptions = new();
            if (ApplicationConfig.RewriteConfig.Enable)
            {
                rewriteOptions.Add(new RewriteHomeIndexRequests(ApplicationConfig.RewriteConfig.Address));
            }
            else if (WebAPIConfig.EnableSwagger)
            {
                rewriteOptions.Add(new RedirectHomeIndexRequests("/swagger/index.html"));
            }
            if (rewriteOptions.Rules.Count > 0)
            {
                app.UseRewriter(rewriteOptions);
            }
            #endregion
            #region 处理静态文件
            app.Use(async (httpContext, next) =>
            {
                if (httpContext.Request.Path.HasValue && !string.IsNullOrWhiteSpace(httpContext.Request.Path.Value))
                {
                    if (ApplicationConfig.ApplicationNameWhiteList.Any(item => httpContext.Request.Path.Value.StartsWith($"/{item}")))
                    {
                        await next();
                    }
                    else
                    {
                        IApplicationInfoService applicationInfoService = MateralServices.GetService<IApplicationInfoService>();
                        string[] paths = httpContext.Request.Path.Value.Split("/");
                        if (paths.Length >= 2 && applicationInfoService.IsRunningApplication(paths[1]))
                        {
                            await next();
                        }
                        else
                        {
                            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        }
                    }
                }
            });
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(basePath, "Application");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileExtensionContentTypeProvider provider = new();
            provider.Mappings[".json"] = "application/json";
            provider.Mappings[".woff"] = "application/font-woff";
            provider.Mappings[".woff2"] = "application/font-woff";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                    context.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                },
                RequestPath = "",
                ContentTypeProvider = provider,
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream"
            });
            #endregion
            MigrateHelper<DeployDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<DeployDBContext>>();
            await migrateHelper.MigrateAsync();
            await app.RunAsync();
        }
    }
}