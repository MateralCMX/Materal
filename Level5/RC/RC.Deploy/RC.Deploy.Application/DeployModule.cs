using Materal.MergeBlock.Abstractions.WebModule;
using Materal.MergeBlock.Application.WebModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using RC.Deploy.Abstractions.Services.Models;
using RC.Deploy.Application.Hubs;
using System.Linq;

namespace RC.Deploy.Application
{
    /// <summary>
    /// Deploy模块
    /// </summary>
    public class DeployModule() : RCModule("RCDeploy模块", "RC.Deploy", ["RC.Deploy.Repository", "Authorization"])
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            await base.OnConfigServiceAsync(context);
            context.Services.TryAddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
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
            context.WebApplication.UseMiddleware<DeployMiddleware>();
            context.WebApplication.UseMiddleware<BrotliStaticFilesMiddleware>();
            context.WebApplication.UseMiddleware<GZipStaticFilesMiddleware>();
            string basePath = typeof(DeployModule).Assembly.GetDirectoryPath();
            string path = Path.Combine(basePath, "Application");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StaticFileOptions applicationStaticFileOptions = GetStaticFileOptions(path, "");
            context.WebApplication.UseStaticFiles(applicationStaticFileOptions);
            path = Path.Combine(basePath, "UploadFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StaticFileOptions uploadFilesStaticFileOptions = GetStaticFileOptions(path, "/UploadFiles");
            context.WebApplication.UseStaticFiles(uploadFilesStaticFileOptions);
            #endregion
            await base.OnApplicationInitAsync(context);
        }
        private StaticFileOptions GetStaticFileOptions(string path, string requestPath, IFileProvider? fileProvider = null)
        {
            FileExtensionContentTypeProvider provider = new();
            provider.Mappings[".json"] = "application/json";
            provider.Mappings[".woff"] = "application/font-woff";
            provider.Mappings[".woff2"] = "application/font-woff";
            fileProvider ??= new PhysicalFileProvider(path);
            return new StaticFileOptions
            {
                FileProvider = fileProvider,
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