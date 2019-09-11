using Demo.Common;
using Demo.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Reflection;

namespace Demo.DependencyInjection
{
    public static class DemoDIExtension
    {
        /// <summary>
        /// 添加权限服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddDemoServices(this IServiceCollection services)
        {
            services.AddSingleton(DemoConfig.DemoDBConfig.SubordinateConfigs);
            services.AddSingleton<Action<DbContextOptionsBuilder, string>>((options, configString) => options.UseSqlServer(configString, m =>
            {
                m.EnableRetryOnFailure();
            }));
            services.AddDbContext<DemoDbContext>(options => options.UseSqlServer(DemoConfig.DemoDBConfig.ConnectionString, m =>
            {
                m.EnableRetryOnFailure();
            }));
            services.AddTransient(typeof(IDemoUnitOfWork), typeof(DemoUnitOfWorkImpl));
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Demo.EFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Demo.MongoDBRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Demo.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
        }
    }
}
