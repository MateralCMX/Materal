using Materal.MergeBlock.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using RC.Deploy.Application.Hubs;

namespace RC.Deploy.Application
{
    /// <summary>
    /// Deploy模块
    /// </summary>
    public class DeployModule : RCModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            context.Services.AddSignalR();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnApplicationInitAsync(IApplicationContext context)
        {
            IOptionsMonitor<ApplicationConfig> applicationConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<ApplicationConfig>>();
            if (context.ApplicationBuilder is WebApplication app)
            {
                app.MapHub<ConsoleMessageHub>("/hubs/ConsoleMessage");
                #region 处理重写
                RewriteOptions rewriteOptions = new();
                if (applicationConfig.CurrentValue.RewriteConfig.Enable)
                {
                    rewriteOptions.Add(new RewriteHomeIndexRequests(applicationConfig.CurrentValue.RewriteConfig.Address));
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
                            IApplicationInfoService applicationInfoService = MateralServices.GetRequiredService<IApplicationInfoService>();
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
                        context.Context.Response.Headers.Append("Access-Control-Allow-Methods", "*");
                        context.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                        context.Context.Response.Headers.Append("Access-Control-Allow-Headers", "*");
                    },
                    RequestPath = "",
                    ContentTypeProvider = provider,
                    ServeUnknownFileTypes = true,
                    DefaultContentType = "application/octet-stream"
                });
                path = Path.Combine(basePath, "UploadFiles");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(path),
                    OnPrepareResponse = context =>
                    {
                        context.Context.Response.Headers.Append("Access-Control-Allow-Methods", "*");
                        context.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                        context.Context.Response.Headers.Append("Access-Control-Allow-Headers", "*");
                    },
                    RequestPath = "/UploadFiles",
                    ContentTypeProvider = provider,
                    ServeUnknownFileTypes = true,
                    DefaultContentType = "application/octet-stream"
                });
                #endregion
            }
            return base.OnApplicationInitAsync(context);
        }
    }
}
