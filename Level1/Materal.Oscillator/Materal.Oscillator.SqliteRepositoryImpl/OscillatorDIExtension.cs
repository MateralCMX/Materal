using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.SqliteRepositoryImpl;
using Materal.TTA.EFRepository;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Materal.Oscillator
{
    public static class OscillatorDIExtension
    {
        /// <summary>
        /// 添加OscillatorSqlite仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="useMemoryRepositories">使用内存仓储</param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorSqliteRepositoriesService(this IServiceCollection services, SqliteConfigModel dbConfig)
        {
            services.AddDbContext<OscillatorSqliteDBContext>(options =>
            {
                options.UseSqlite(dbConfig.ConnectionString, m =>
                {
                    m.CommandTimeout(300);
                });
            });
            services.AddTransient<MigrateHelper<OscillatorSqliteDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.Oscillator.SqliteRepositoryImpl"))
                .Where(m => m.Name.EndsWith("RepositoryImpl") && m.IsClass && !m.IsAbstract)
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.AddTransient<IOscillatorUnitOfWork, OscillatorSqliteUnitOfWorkImpl>();
            return services;
        }
    }
}
