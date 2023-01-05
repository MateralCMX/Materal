using Materal.TTA.EFRepository;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RC.Core.SqliteEFRepository;

namespace RC.Core.SqlServer
{
    /// <summary>
    /// 数据库依赖注入扩展
    /// </summary>
    public static class DBDIExtension
    {
        /// <summary>
        /// 添加数据库服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig"></param>
        /// <param name="serviceLifetime"></param>
        public static IServiceCollection AddDataBaseServices<T>(this IServiceCollection services, SqliteConfigModel dbConfig, ServiceLifetime serviceLifetime)
            where T : DbContext
        {
            services.AddDbContext<T>(options =>
            {
                options.UseSqlite(dbConfig.ConnectionString, m =>
                {
                    m.CommandTimeout(300);
                });
            }, serviceLifetime);
            services.AddTransient<MigrateHelper<T>>();
            services.AddTransient<IRCUnitOfWork, RCUnitOfWorkImpl<T>>();
            return services;
        }
    }
}
