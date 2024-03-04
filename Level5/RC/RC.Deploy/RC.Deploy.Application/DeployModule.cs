using Consul;
using Materal.MergeBlock.Abstractions.WebModule;
using Materal.MergeBlock.Application.WebModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using RC.Deploy.Abstractions.Services.Models;
using RC.Deploy.Application.Hubs;

namespace RC.Deploy.Application
{
    /// <summary>
    /// Deploy模块
    /// </summary>
    public class DeployModule : RCModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DeployModule() : base("RCDeploy模块", "RC.Deploy", ["RC.Deploy.Repository", "Authorization"])
        {
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            await base.OnConfigServiceAsync(context);
            context.Services.AddSignalR();
            IConfigurationSection configurationSection = context.Configuration.GetSection("Deploy");
            context.Services.Configure<ApplicationConfig>(configurationSection);
            string? serviceName = configurationSection.GetConfigItemToString("ServiceName") ?? "RCDeploy";
            string? serviceDescription = configurationSection.GetConfigItemToString("ServiceDescription") ?? "RC发布服务";
            context.Services.AddConsulConfig(serviceName, ["RC.Deploy", serviceDescription]);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IWebApplicationContext context)
        {
            context.WebApplication.MapHub<ConsoleMessageHub>("/DeployHubs/ConsoleMessage");
            IOptionsMonitor<ApplicationConfig> applicationConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<ApplicationConfig>>();
            #region 处理重写
            RewriteOptions rewriteOptions = new();
            if (applicationConfig.CurrentValue.RewriteConfig.Enable)
            {
                rewriteOptions.Add(new RewriteHomeIndexRequests(applicationConfig.CurrentValue.RewriteConfig.Address));
            }
            if (rewriteOptions.Rules.Count > 0)
            {
                context.WebApplication.UseRewriter(rewriteOptions);
            }
            #endregion
            #region 处理静态文件
            context.WebApplication.Use(async (httpContext, next) =>
            {
                if (!httpContext.Request.Path.HasValue || string.IsNullOrWhiteSpace(httpContext.Request.Path.Value)) return;
                IApplicationInfoService applicationInfoService = context.ServiceProvider.GetRequiredService<IApplicationInfoService>();
                string[] paths = httpContext.Request.Path.Value.Split("/");
                IApplicationRuntimeModel? applicationRuntimeModel = applicationInfoService.GetApplicationRuntimeModel(paths[1]);
                if (applicationRuntimeModel is null ||
                    applicationRuntimeModel.ApplicationInfo.ApplicationType != ApplicationTypeEnum.StaticDocument ||
                    applicationRuntimeModel.ApplicationStatus == ApplicationStatusEnum.Runing)
                {
                    await next();
                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            });
            string basePath = typeof(DeployModule).Assembly.GetDirectoryPath();
            string path = Path.Combine(basePath, "Application");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            context.WebApplication.UseStaticFiles(GetStaticFileOptions(path, ""));
            path = Path.Combine(basePath, "UploadFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            context.WebApplication.UseStaticFiles(GetStaticFileOptions(path, "/UploadFiles"));
            #endregion
            await base.OnApplicationInitAsync(context);
        }
        private StaticFileOptions GetStaticFileOptions(string path, string requestPath)
        {
            FileExtensionContentTypeProvider provider = new();
            provider.Mappings[".json"] = "application/json";
            provider.Mappings[".woff"] = "application/font-woff";
            provider.Mappings[".woff2"] = "application/font-woff";
            return new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                OnPrepareResponse = OnPrepareResponse,
                RequestPath = requestPath,
                ContentTypeProvider = provider,
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream"
            };
        }
        private void OnPrepareResponse(StaticFileResponseContext context)
        {
            context.Context.Response.Headers.Append("Access-Control-Allow-Methods", "*");
            context.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            context.Context.Response.Headers.Append("Access-Control-Allow-Headers", "*");
        }
    }
}