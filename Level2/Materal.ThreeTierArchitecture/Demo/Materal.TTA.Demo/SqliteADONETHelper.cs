using Materal.Abstractions;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Demo.SqliteADONETRepository;
using Materal.TTA.SqliteADONETRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TTA.Demo
{
    public static class SqliteADONETHelper
    {
        public static IServiceCollection AddSqliteADONETTTA(this IServiceCollection services)
        {
            DemoDBOption option = new(DBConfig.SqliteConfig);
            services.AddTTASqliteADONETRepository(option);
            return services;
        }
        public static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            IMigrateHelper<DemoDBOption> migrateHelper = serviceProvider.GetService<IMigrateHelper<DemoDBOption>>() ?? throw new MateralException("获取实例失败");
            await migrateHelper.MigrateAsync();
        }
    }
}
