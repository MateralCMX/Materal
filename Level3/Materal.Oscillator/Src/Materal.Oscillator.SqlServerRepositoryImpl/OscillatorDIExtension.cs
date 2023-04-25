using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.SqlServerRepositoryImpl;
using Materal.TTA.EFRepository;
using Materal.TTA.SqlServerRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Materal.Oscillator
{
    public static class OscillatorDIExtension
    {
        /// <summary>
        /// 添加OscillatorSqlServer仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="useMemoryRepositories">使用内存仓储</param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorSqlServerRepositoriesService(this IServiceCollection services, SqlServerConfigModel dbConfig)
        {
            services.AddDbContext<OscillatorSqlServerDBContext>(options =>
            {
                options.UseSqlServer(dbConfig.ConnectionString, m =>
                {
                    m.EnableRetryOnFailure();
                    m.CommandTimeout(300);
                });
            });
            services.AddScoped<MigrateHelper<OscillatorSqlServerDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.Oscillator.SqlServerRepositoryImpl"))
                .Where(m => m.Name.EndsWith("RepositoryImpl") && m.IsClass && !m.IsAbstract)
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.AddScoped<IOscillatorUnitOfWork, OscillatorSqlServerUnitOfWorkImpl>();
            return services;
        }
    }
}
