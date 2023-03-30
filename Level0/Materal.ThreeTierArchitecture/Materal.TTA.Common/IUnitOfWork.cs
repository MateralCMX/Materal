namespace Materal.TTA.Common
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// DI服务
        /// </summary>
        IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
        /// <summary>
        /// 提交
        /// </summary>
        void Commit();
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterAdd<T, TPrimaryKeyType>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct;
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterEdit<T, TPrimaryKeyType>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterDelete<T, TPrimaryKeyType>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct;
        /// <summary>
        /// 获得仓储
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>()
            where TRepository : IRepository;
    }
}
