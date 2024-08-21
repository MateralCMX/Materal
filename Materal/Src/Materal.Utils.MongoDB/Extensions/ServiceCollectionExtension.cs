namespace Materal.Utils.MongoDB.Extensions
{
    /// <summary>
    /// ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加Mongo工具
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoUtils(this IServiceCollection services)
        {
            services.TryAddTransient<IMongoRepository, MongoRepository>();
            services.TryAddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.TryAddTransient(typeof(IMongoRepository<,>), typeof(MongoRepository<,>));
            return services;
        }
    }
}
