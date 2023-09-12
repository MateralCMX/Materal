using Materal.TTA.Common.Model;
using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MBC.Core.EFRepository
{
    public static class DBDIExtension
    {
        /// <summary>
        /// 添加数据库服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sqliteConfig"></param>
        /// <returns></returns>
        public static IServiceCollection AddDBService<T>(this IServiceCollection services, SqliteConfigModel dbConfig)
            where T : DbContext
        {
            services.AddTTASqliteEFRepository<T>(dbConfig.ConnectionString);
            return services;
        }
    }
}
