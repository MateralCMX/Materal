using Materal.BaseCore.EFRepository;
using Materal.TTA.EFRepository;
using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Materal.TTA.SqliteEFRepository;

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
