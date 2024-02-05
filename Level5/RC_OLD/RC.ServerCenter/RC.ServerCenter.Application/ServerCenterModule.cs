using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace RC.ServerCenter.Application
{
    /// <summary>
    /// ServerCenter模块
    /// </summary>
    public class ServerCenterModule : RCModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServerCenterModule() : base("RCServerCenter模块", "RC.ServerCenter", ["RC.ServerCenter.Repository", "Authorization"])
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
            context.Services.Configure<ApplicationConfig>(context.Configuration.GetSection("RC.ServerCenter"));
            context.Services.AddConsulConfig("RCServerCenter", ["RC.ServerCenter"]);
        }
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IWebApplicationContext context)
        {
            string managementPath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "Management");
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
            await base.OnApplicationInitBeforeAsync(context);
        }
    }
}