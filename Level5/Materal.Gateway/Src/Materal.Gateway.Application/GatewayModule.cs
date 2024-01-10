using Materal.Gateway.OcelotExtension;
using Materal.Gateway.Service;
using Materal.Utils.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Materal.Gateway.Application
{
    /// <summary>
    /// 网关模块
    /// </summary>
    public class GatewayModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ocelot.json");
            if (context.Configuration is ConfigurationManager configuration)
            {
                configuration.AddJsonFile(filePath, false, true); //加载Ocelot配置
            }
            context.Services.AddMateralConsulUtils();
            context.Services.AddSwaggerForOcelot(context.Configuration);
            context.Services.AddOcelotGateway();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            IOcelotConfigService ocelotConfigService = context.ServiceProvider.GetRequiredService<IOcelotConfigService>();
            await ocelotConfigService.InitAsync();
            string managementPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Management");
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
            context.ApplicationBuilder.UseStaticFiles(staticFileOptions);
            await base.OnApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            await context.ApplicationBuilder.UseOcelotGatewayAsync(true);
            IWebHostEnvironment environment = context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            if (environment.IsDevelopment())
            {
                context.ApplicationBuilder.UseSwaggerForOcelotUI(m => m.PathToSwaggerGenerator = "/swagger/docs");
            }
            else
            {
                context.ApplicationBuilder.UseSwaggerForOcelotUI();
            }
            await base.OnApplicationInitAsync(context);
        }
    }
}
