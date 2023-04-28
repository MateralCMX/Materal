using Materal.Abstractions;
using Materal.TTA.Demo.SqlServerEFRepository;
using Materal.TTA.EFRepository;
using Materal.TTA.SqlServerEFRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.TTA.Demo
{
    public static class SqlServerEFHelper
    {
        public static IServiceCollection AddSqlServerEFTTA(this IServiceCollection services)
        {
            services.AddTTASqlServerEFRepository<TTADemoDBContext>(DBConfig.SqlServerConfig.ConnectionString, Assembly.Load("Materal.TTA.Demo.SqlServerEFRepository"));
            return services;
        }
        public static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            IMigrateHelper<TTADemoDBContext> migrateHelper = serviceProvider.GetService<IMigrateHelper<TTADemoDBContext>>() ?? throw new MateralException("获取实例失败");
            await migrateHelper.MigrateAsync();
        }
    }
}
