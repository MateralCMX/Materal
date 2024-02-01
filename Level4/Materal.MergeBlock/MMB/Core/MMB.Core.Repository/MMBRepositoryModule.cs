namespace MMB.Core.Repository
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
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            var moduleType = GetType();
            string configFilePath = moduleType.Assembly.GetDirectoryPath();
            configFilePath = Path.Combine(configFilePath, $"{moduleType.Namespace}.json");
            context.Configuration.AddJsonFile(configFilePath, optional: true, reloadOnChange: true);
            await base.OnConfigServiceAsync(context);
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
