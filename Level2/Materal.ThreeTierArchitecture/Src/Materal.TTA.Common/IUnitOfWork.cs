namespace Materal.TTA.Common
{
    /// <summary>
    /// 工作单元
    /// </summary>
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
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct;
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct;
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        void RegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct;
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        void RegisterDelete<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterDelete<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
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
