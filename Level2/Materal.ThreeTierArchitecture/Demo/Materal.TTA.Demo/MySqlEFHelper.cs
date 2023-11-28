#if NET6_0
using Materal.Abstractions;
using Materal.TTA.Demo.MySqlEFRepository;
using Materal.TTA.EFRepository;
using Materal.TTA.MySqlEFRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TTA.Demo
{
    public static class MySqlEFHelper
    {
        public static IServiceCollection AddMySqlEFTTA(this IServiceCollection services)
        {
            services.AddTTAMySqlEFRepository<TTADemoDBContext>(DBConfig.MySqlConfig.ConnectionString);
            return services;
        }
        public static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            IMigrateHelper<TTADemoDBContext> migrateHelper = serviceProvider.GetService<IMigrateHelper<TTADemoDBContext>>() ?? throw new MateralException("获取实例失败");
            await migrateHelper.MigrateAsync();
        }
    }
}
#endif