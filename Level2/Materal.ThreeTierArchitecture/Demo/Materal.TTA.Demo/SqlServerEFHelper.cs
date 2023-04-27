using Materal.Abstractions;
using Materal.TTA.Common.Model;
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
            SqlServerConfigModel dbConfig = new()
            {
                Address = "175.27.194.19",
                Port = "1433",
                Name = "TTATestDB",
                UserID = "sa",
                Password = "XMJry@456",
                TrustServerCertificate = true
            };
            services.AddTTASqlServerEFRepository<TTADemoDBContext>(dbConfig.ConnectionString, Assembly.Load("Materal.TTA.Demo.SqlServerEFRepository"));
            return services;
        }
        public static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            MigrateHelper<TTADemoDBContext> migrateHelper = serviceProvider.GetService<MigrateHelper<TTADemoDBContext>>() ?? throw new MateralException("获取实例失败");
            await migrateHelper.MigrateAsync();
        }
    }
}
