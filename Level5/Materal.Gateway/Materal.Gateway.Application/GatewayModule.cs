using Materal.Gateway.Application.Extensions;
using Materal.Gateway.Service;
using Materal.MergeBlock.Abstractions.WebModule;
using Materal.Utils.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace Materal.Gateway.Application
{
    /// <summary>
    /// 网关模块
    /// </summary>
    public class GatewayModule() : MergeBlockWebModule("网关模块", "Gateway")
    {
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IWebConfigServiceContext context)
        {
            string filePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "Ocelot.json");
            if (context.Configuration is ConfigurationManager configuration)
            {
                configuration.AddJsonFile(filePath, false, true);
            }
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            context.Services.AddMateralConsulUtils();
            context.Services.AddSwaggerForOcelot(context.Configuration);
            context.Services.AddMateralGateway();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IWebApplicationContext context)
        {
            string managementPath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "GatewayManagement");
            DirectoryInfo managementDirectoryInfo = new(managementPath);
            if (!managementDirectoryInfo.Exists)
            {
                managementDirectoryInfo.Create();
                managementDirectoryInfo.Refresh();
            }
            StaticFileOptions staticFileOptions = new()
            {
                FileProvider = new PhysicalFileProvider(managementDirectoryInfo.FullName),
                RequestPath = $"/{managementDirectoryInfo.Name}",
            };
            context.WebApplication.UseStaticFiles(staticFileOptions);
            context.ServiceProvider.GetService<ILogger<GatewayModule>>()?.LogInformation($"已启用网关管理界面:/{managementDirectoryInfo.Name}/Index.html");
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IWebApplicationContext context)
        {
            context.WebApplication.UseMateralGatewaySwaggerUI();
            await context.WebApplication.UseMateralGatewayAsync();
            IOcelotConfigService ocelotConfigService = context.ServiceProvider.GetRequiredService<IOcelotConfigService>();
            await ocelotConfigService.InitAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}
