using System;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.ApplicationUpdate.DependencyInjection
{
    public static class FrameworkDependencyInjectionExtension
    {
        /// <summary>
        /// 添加框架
        /// </summary>
        /// <param name="services"></param>
        public static void AddFrameWorkTransient(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IEfRepository<,>), typeof(EfRepositoryImpl<,>));
            //services.AddTransient(typeof(IUserEfRepository<,>), typeof(UserEfRepositoryImpl<,>));
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IUserUnitOfWork, UserUnitOfWork>();
            //services.AddTransient<ICacheService, MemoryCacheServiceImpl>();
        }
    }
}
