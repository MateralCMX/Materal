﻿namespace RC.Core.Repository
{
    /// <summary>
    /// RC仓储模块
    /// </summary>
    public abstract class RCRepositoryModule<TDBContext>(string description, string? moduleName = null, string[]? depends = null) : RepositoryModule<TDBContext, SqliteConfigModel>(description, moduleName, depends)
        where TDBContext : DbContext
    {
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