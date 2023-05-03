using Materal.Abstractions;
using Materal.TTA.Demo.SqliteEFRepository;
using Materal.TTA.EFRepository;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TTA.Demo
{
    public static class SqliteEFHelper
    {
        public static IServiceCollection AddSqliteEFTTA(this IServiceCollection services)
        {
            services.AddTTASqliteEFRepository<TTADemoDBContext>(DBConfig.SqliteConfig.ConnectionString);
            return services;
        }
        public static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            IMigrateHelper<TTADemoDBContext> migrateHelper = serviceProvider.GetService<IMigrateHelper<TTADemoDBContext>>() ?? throw new MateralException("获取实例失败");
            await migrateHelper.MigrateAsync();
        }
    }
}
