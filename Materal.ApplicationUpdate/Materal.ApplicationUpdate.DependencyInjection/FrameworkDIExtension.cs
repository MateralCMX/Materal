using Materal.ApplicationUpdate.EFRepository;
using Materal.CacheHelper;
using Materal.TTA.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.ApplicationUpdate.DependencyInjection
{
    /// <summary>
    /// 框架依赖注入扩展
    /// </summary>
    public static class FrameworkDIExtension
    {
        /// <summary>
        /// 添加框架服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddFrameWorkServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, AppUpdateUnitOfWorkImpl>();
            services.AddTransient<IAppUpdateUnitOfWork, AppUpdateUnitOfWorkImpl>();
            services.AddTransient<ICacheHelper, MemoryCacheHelper>();
        }
    }
}
