using Materal.Gateway.OcelotExtension;
using Materal.Gateway.Service;
using Materal.MergeBlock.Abstractions.WebModule;
using Materal.Utils.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Materal.Gateway.Application
{
    /// <summary>
    /// 网关模块
    /// </summary>
    public class GatewayModule : MergeBlockWebModule, IMergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public GatewayModule() : base("网关模块", "Gateway")
        {

        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            string filePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "Ocelot.json");
            if (context.Configuration is ConfigurationManager configuration)
            {
                configuration.AddJsonFile(filePath, false, true);
            }
            return base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IWebConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            context.Services.AddMateralConsulUtils();
            context.Services.AddSwaggerForOcelot(context.Configuration);
            context.Services.AddOcelotGateway();
            //context.MvcBuilder.AddApplicationPart(typeof(GlobalRateLimitOptionsController).Assembly);
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IWebApplicationContext context)
        {
            IOcelotConfigService ocelotConfigService = context.ServiceProvider.GetRequiredService<IOcelotConfigService>();
            await ocelotConfigService.InitAsync();
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
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IWebApplicationContext context)
        {
            IWebHostEnvironment environment = context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            if (environment.IsDevelopment())
            {
                context.WebApplication.UseSwaggerForOcelotUI(m => m.PathToSwaggerGenerator = "/swagger/docs");
            }
            else
            {
                context.WebApplication.UseSwaggerForOcelotUI();
            }
            await context.WebApplication.UseOcelotGatewayAsync(true);
            await base.OnApplicationInitAsync(context);
        }
    }
}
