using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.DR;
using Materal.Oscillator.DR.Repositories;
using Materal.Oscillator.SqliteRepositoryImpl;
using Materal.TTA.EFRepository;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Materal.Oscillator.LocalDR
{
    public static class OscillatorDRDIExtension
    {
        /// <summary>
        /// 添加Oscillator本地容灾服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorLocalDRService(this IServiceCollection services, SqliteConfigModel dbConfig, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            services.AddDbContext<OscillatorLocalDRDBContext>(options =>
            {
                options.UseSqlite(dbConfig.ConnectionString, m =>
                {
                    m.CommandTimeout(300);
                });
            }, serviceLifetime);
            services.AddTransient<MigrateHelper<OscillatorLocalDRDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.Oscillator.LocalDR"))
                .Where(m => m.Name.EndsWith("RepositoryImpl") && m.IsClass && !m.IsAbstract)
                .AsPublicImplementedInterfaces();
            services.AddTransient<IOscillatorDRUnitOfWork, OscillatorLocalDRUnitOfWorkImpl>();
            services.AddTransient<IOscillatorDR, OscillatorLocalDR>();
            return services;
        }
    }
}
