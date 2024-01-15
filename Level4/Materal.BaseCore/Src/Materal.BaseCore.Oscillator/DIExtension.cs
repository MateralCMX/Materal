using Materal.BaseCore.Common;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Extensions;
using Materal.Oscillator.SqliteEFRepository.Extensions;
using Materal.Oscillator.SqlServerEFRepository.Extensions;
using Materal.TTA.Common.Model;
using Materal.TTA.SqliteEFRepository;
using Materal.TTA.SqlServerEFRepository;

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
            OscillatorDIExtension.AddOscillator(services, scheduleAssemblies);
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
            List<IOscillatorSchedule> schedules = [];
            foreach (Assembly scheduleAssembly in scheduleAssemblies)
            {
                Type[] oscillatorScheduleTypes = scheduleAssembly.GetTypes().Where(m => !m.IsAbstract && m.IsClass && m.IsAssignableTo<IOscillatorSchedule>()).ToArray();
                foreach (Type oscillatorScheduleType in oscillatorScheduleTypes)
                {
                    object obj = oscillatorScheduleType.Instantiation([]);
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
