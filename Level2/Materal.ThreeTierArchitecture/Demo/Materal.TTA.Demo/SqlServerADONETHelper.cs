using Materal.Abstractions;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Demo.SqlServerADONETRepository;
using Materal.TTA.SqlServerADONETRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.TTA.Demo
{
    public static class SqlServerADONETHelper
    {
        public static IServiceCollection AddSqlServerADONETTTA(this IServiceCollection services)
        {
            DemoDBOption option = new(DBConfig.SqlServerConfig);
            services.AddTTASqlServerADONETRepository(option, Assembly.Load("Materal.TTA.Demo.SqlServerADONETRepository"));
            return services;
        }
        public static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            IMigrateHelper<DemoDBOption> migrateHelper = serviceProvider.GetService<IMigrateHelper<DemoDBOption>>() ?? throw new MateralException("获取实例失败");
            await migrateHelper.MigrateAsync();
        }
    }
}
