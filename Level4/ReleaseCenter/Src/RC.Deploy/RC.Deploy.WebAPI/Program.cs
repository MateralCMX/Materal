using Materal.Abstractions;
using Materal.BaseCore.WebAPI;
using Materal.BaseCore.WebAPI.Common;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using RC.Deploy.Common;
using RC.Deploy.Hubs;
using RC.Deploy.ServiceImpl;
using RC.Deploy.Services;

namespace RC.Deploy.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigService(IServiceCollection services)
        {
            base.ConfigService(services);
            services.AddSignalR();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public override async Task InitAsync(string[] args, IServiceProvider services, WebApplication app)
        {
            AppDomain.CurrentDomain.ProcessExit += Deploy_ProcessExit;
            app.MapHub<ConsoleMessageHub>("/hubs/ConsoleMessage");
            #region 处理重写
            RewriteOptions rewriteOptions = new();
            if (ApplicationConfig.RewriteConfig.Enable)
            {
                rewriteOptions.Add(new RewriteHomeIndexRequests(ApplicationConfig.RewriteConfig.Address));
            }
            else if (WebAPIConfig.SwaggerConfig.Enable)
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
                    context.Context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                    context.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                },
                RequestPath = "/UploadFiles",
                ContentTypeProvider = provider,
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream"
            });
            #endregion
            await base.InitAsync(args, services, app);
        }
        /// <summary>
        /// 程序退出时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void Deploy_ProcessExit(object? sender, EventArgs e)
        {
            ApplicationRuntimeManage.ShutDownAsync().Wait();
        }
    }
}