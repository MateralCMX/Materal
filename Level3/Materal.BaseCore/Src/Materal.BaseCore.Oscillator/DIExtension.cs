using Materal.BaseCore.Common;
using Materal.Oscillator;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.SqliteEFRepository;
using Materal.Oscillator.SqlServerEFRepository;
using Materal.TTA.Common.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.BaseCore.Oscillator
{
    public static class DIExtension
    {
        /// <summary>
        /// 添加调度器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="scheduleAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillator(this IServiceCollection services, params Assembly[] scheduleAssemblies)
        {
            IDBConfigModel dBConfigModel = new SqliteConfigModel() { Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Oscillator.db") };
            services.AddOscillator(dBConfigModel, scheduleAssemblies);
            return services;
        }
        /// <summary>
        /// 添加调度器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dBConfigModel"></param>
        /// <param name="scheduleAssemblies"></param>
        /// <returns></returns>
        /// <exception cref="MateralCoreException"></exception>
        public static IServiceCollection AddOscillator(this IServiceCollection services, IDBConfigModel dBConfigModel, params Assembly[] scheduleAssemblies)
        {
            OscillatorDIExtension.AddOscillator(services);
            if(dBConfigModel is SqliteConfigModel sqliteConfig)
            {
                services.AddOscillatorSqliteEFRepository(sqliteConfig);
            }
            else if(dBConfigModel is SqlServerConfigModel sqlServerConfigModel)
            {
                services.AddOscillatorSqlServerEFRepository(sqlServerConfigModel);
            }
            else
            {
                throw new MateralCoreException("未知的数据库类型");
            }
            services.AddSingleton<IOscillatorListener, OscillatorListenerImpl>();
            List<IOscillatorSchedule> schedules = new();
            foreach (Assembly scheduleAssembly in scheduleAssemblies)
            {
                Type[] oscillatorScheduleTypes = scheduleAssembly.GetTypes().Where(m => !m.IsAbstract && m.IsClass && m.IsAssignableTo<IOscillatorSchedule>()).ToArray();
                foreach (Type oscillatorScheduleType in oscillatorScheduleTypes)
                {
                    object obj = oscillatorScheduleType.Instantiation(Array.Empty<object>());
                    if (obj is not IOscillatorSchedule oscillatorSchedule) continue;
                    schedules.Add(oscillatorSchedule);
                }
            }
            services.AddSingleton(schedules);
            services.AddScoped<IOscillatorService, OscillatorServiceImpl>();
            return services;
        }
    }
}
