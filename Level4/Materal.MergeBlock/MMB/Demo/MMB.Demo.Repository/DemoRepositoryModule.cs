using MMB.Demo.Repository.RepositoryImpls;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo仓储模块
    /// </summary>
    public class DemoRepositoryModule : MMBRepositoryModule<DemoDBContext>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DemoRepositoryModule() : base("MMB.Demo仓储模块", "MMB.Demo.Repository")
        {
        }
        /// <summary>
        /// 添加DBContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dBConfig"></param>
        protected override void AddDBContext(IServiceCollection services, SqliteConfigModel dBConfig)
        {
            services.AddDbContext<DemoDBContext>(delegate (DbContextOptionsBuilder options)
            {
                options.UseSqlite(dBConfig.ConnectionString, null).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.TryAddScoped<IMigrateHelper<DemoDBContext>, MigrateHelper<DemoDBContext>>();
            services.TryAddScoped<IDemoUnitOfWork, DemoUnitOfWorkImpl>();
            services.TryAddScoped<IUserRepository, UserRepositoryImpl>();
        }
    }
}
