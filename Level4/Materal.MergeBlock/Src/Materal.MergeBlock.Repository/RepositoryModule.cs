namespace Materal.MergeBlock.Repository
{
    /// <summary>
    /// 仓储模块
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDBConfigType"></typeparam>
    public abstract class RepositoryModule<T, TDBConfigType> : MergeBlockModule, IMergeBlockModule
        where T : DbContext
        where TDBConfigType : IDBConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        protected abstract string ConfigKey { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="moduleName"></param>
        /// <param name="depends"></param>
        protected RepositoryModule(string description, string? moduleName = null, string[]? depends = null) : base(description, moduleName, depends)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="depends"></param>
        protected RepositoryModule(string description, string[]? depends) : base(description, depends)
        {
        }
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
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            TDBConfigType dbConfig = context.Configuration.GetValueObject<TDBConfigType>(ConfigKey) ?? throw new MergeBlockException($"获取数据库配置[{ConfigKey}]失败");
            AddDBContext(context.Services, dbConfig);
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            using IServiceScope serviceScope = context.ServiceProvider.CreateScope();
            IMigrateHelper migrateHelper = serviceScope.ServiceProvider.GetRequiredService<IMigrateHelper<T>>();
            await migrateHelper.MigrateAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}
