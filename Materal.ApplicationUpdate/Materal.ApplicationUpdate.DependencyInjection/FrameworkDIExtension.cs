using Materal.ApplicationUpdate.EFRepository;
using Materal.CacheHelper;
using Materal.TTA.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.ApplicationUpdate.DependencyInjection
{
    public static class FrameworkDIExtension
    {
        /// <summary>
        /// 添加框架
        /// </summary>
        /// <param name="services"></param>
        public static void AddFrameWorkServices(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IEfRepository<,>), typeof(EfRepositoryImpl<,>));
            //services.AddTransient(typeof(IUserEfRepository<,>), typeof(UserEfRepositoryImpl<,>));
            services.AddTransient<IUnitOfWork, AppUpdateUnitOfWorkImpl>();
            services.AddTransient<IAppUpdateUnitOfWork, AppUpdateUnitOfWorkImpl>();
            //services.AddTransient<IUserUnitOfWork, UserUnitOfWork>();
            services.AddTransient<ICacheHelper, MemoryCacheHelper>();
        }
    }
}
