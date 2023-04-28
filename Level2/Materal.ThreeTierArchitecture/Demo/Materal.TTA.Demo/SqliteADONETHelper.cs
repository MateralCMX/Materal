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
            DemoDBOption option = new(DBConfig.SqliteConfig);
            services.AddTTASqliteADONETRepository(option, Assembly.Load("Materal.TTA.Demo.SqliteADONETRepository"));
            return services;
        }
    }
}
