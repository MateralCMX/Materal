using Materal.TTA.Common.Model;
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
            SqlServerConfigModel dbConfig = new()
            {
                Address = "175.27.194.19",
                Port = "1433",
                Name = "TTATestDB",
                UserID = "sa",
                Password = "XMJry@456",
                TrustServerCertificate = true
            };
            DemoDBConfig config = new()
            {
                ConnectionString = dbConfig.ConnectionString
            };
            services.AddTTASqlServerADONETRepository(config, Assembly.Load("Materal.TTA.Demo.SqlServerADONETRepository"));
            return services;
        }
    }
}
