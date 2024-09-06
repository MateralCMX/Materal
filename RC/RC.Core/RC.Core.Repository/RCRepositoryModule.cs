﻿namespace RC.Core.Repository
{
    /// <summary>
    /// RC仓储模块
    /// </summary>
    public abstract class RCRepositoryModule<TDBContext>(string moduleName) : RepositoryModule<TDBContext, SqliteConfigModel>(moduleName)
        where TDBContext : DbContext
    {
        /// <inheritdoc/>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            if (context.Configuration is not IConfigurationBuilder configurationBuilder) return;
            Type moduleType = GetType();
            string configFilePath = moduleType.Assembly.GetDirectoryPath();
            configFilePath = Path.Combine(configFilePath, $"{moduleType.Namespace}.json");
            configurationBuilder.AddJsonFile(configFilePath, optional: true, reloadOnChange: true);
            base.OnConfigureServices(context);
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