using Materal.TTA.Common.Model;
using Materal.TTA.Demo.SqliteADONETRepository;
using Materal.TTA.SqliteADONETRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.TTA.Demo
{
    public static class SqliteADONETHelper
    {
        public static IServiceCollection AddSqliteADONETTTA(this IServiceCollection services)
        {
            SqliteConfigModel dbConfig = new()
            {
                Source = "TTATestDB.db"
            };
            DemoDBOption config = new(dbConfig); ;
            services.AddTTASqliteADONETRepository(config, Assembly.Load("Materal.TTA.Demo.SqliteADONETRepository"));
            return services;
        }
    }
}
