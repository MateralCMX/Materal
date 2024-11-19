using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.Repository.Abstractions
{
    /// <summary>
    /// 仓储模块
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDBConfigType"></typeparam>
    public abstract class RepositoryModule<T, TDBConfigType>(string moduleName) : MergeBlockModule(moduleName), IMergeBlockModule
        where T : DbContext
        where TDBConfigType : IDBConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        protected abstract string ConfigKey { get; }
        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dBConfig"></param>
        protected abstract void AddDBContext(IServiceCollection services, TDBConfigType dBConfig);
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="MergeBlockException"></exception>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            if (context.Configuration is null) return;
            TDBConfigType? dbConfig = context.Configuration.GetConfigItem<TDBConfigType>(ConfigKey);
            if (dbConfig is null) return;
            AddDBContext(context.Services, dbConfig);
        }
        /// <inheritdoc/>
        public override async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            using IServiceScope serviceScope = context.ServiceProvider.CreateScope();
            IMigrateHelper migrateHelper = serviceScope.ServiceProvider.GetRequiredService<IMigrateHelper<T>>();
            await migrateHelper.MigrateAsync();
        }
    }
}
