using Materal.MergeBlock.Consul.Abstractions;
using Materal.MergeBlock.Web.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using RC.Deploy.Application.Hubs;
using RC.Deploy.Repository;

namespace RC.Deploy.Application
{
    /// <summary>
    /// Deploy模块
    /// </summary>
    [DependsOn(typeof(DeployRepositoryModule))]
    public class DeployModule() : RCModule("RCDeploy模块")
    {
        /// <inheritdoc/>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            base.OnConfigureServices(context);
            context.Services.TryAddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
            context.Services.AddSignalR();
            if (context.Configuration is null) return;
            IConfigurationSection configurationSection = context.Configuration.GetSection("Deploy");
            string? serviceName = configurationSection.GetConfigItemToString("ServiceName") ?? "RCDeploy";
            string? serviceDescription = configurationSection.GetConfigItemToString("ServiceDescription") ?? "RC发布服务";
            context.Services.AddConsulConfig(serviceName, ["RC.Deploy", serviceDescription]);
        }
        /// <inheritdoc/>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            if (context.ServiceProvider.GetRequiredService<AdvancedContext>().App is not WebApplication webApplication) return;
            webApplication.MapHub<ConsoleMessageHub>("/DeployHubs/ConsoleMessage");
            IOptionsMonitor<ApplicationConfig> applicationConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<ApplicationConfig>>();
            #region 处理重写
            RewriteOptions rewriteOptions = new();
            if (applicationConfig.CurrentValue.RewriteConfig.Enable)
            {
                rewriteOptions.Add(new RewriteHomeIndexRequests(applicationConfig.CurrentValue.RewriteConfig.Address));
            }
            if (rewriteOptions.Rules.Count > 0)
            {
                webApplication.UseRewriter(rewriteOptions);
            }
            #endregion
            #region 处理静态文件
            webApplication.UseMiddleware<DeployMiddleware>();
            webApplication.UseMiddleware<BrotliStaticFilesMiddleware>();
            webApplication.UseMiddleware<GZipStaticFilesMiddleware>();
            string basePath = typeof(DeployModule).Assembly.GetDirectoryPath();
            string path = Path.Combine(basePath, "Application");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StaticFileOptions applicationStaticFileOptions = GetStaticFileOptions(path, "");
            webApplication.UseStaticFiles(applicationStaticFileOptions);
            path = Path.Combine(basePath, "UploadFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StaticFileOptions uploadFilesStaticFileOptions = GetStaticFileOptions(path, "/UploadFiles");
            webApplication.UseStaticFiles(uploadFilesStaticFileOptions);
            #endregion
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