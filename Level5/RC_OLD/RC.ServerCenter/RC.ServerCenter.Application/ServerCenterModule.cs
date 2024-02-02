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
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
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
    }
}
