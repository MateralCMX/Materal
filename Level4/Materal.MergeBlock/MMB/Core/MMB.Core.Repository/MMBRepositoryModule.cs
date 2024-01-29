using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// MMB仓储模块
    /// </summary>
    public abstract class MMBRepositoryModule<TDBContext> : RepositoryModule<TDBContext, SqliteConfigModel>
        where TDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="depends"></param>
        public MMBRepositoryModule(string description, string[]? depends) : base(description, depends)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="moduleName"></param>
        /// <param name="depends"></param>
        public MMBRepositoryModule(string description, string? moduleName = null, string[]? depends = null) : base(description, moduleName, depends)
        {
        }
        /// <summary>
        /// 添加DBContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dBConfig"></param>
        protected override void AddDBContext(IServiceCollection services, SqliteConfigModel dBConfig)
        {
            services.AddDbContext<TDBContext>(delegate (DbContextOptionsBuilder options)
            {
                options.UseSqlite(dBConfig.ConnectionString, null).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.TryAddScoped<IMigrateHelper<TDBContext>, MigrateHelper<TDBContext>>();
        }
    }
}
