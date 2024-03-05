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
        /// <param name="setDetached"></param>
        /// <returns></returns>
        Task CommitAsync(bool setDetached = true);
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="setDetached"></param>
        void Commit(bool setDetached = true);
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
        /// 注册添加
        /// </summary>
        /// <param name="obj"></param>
        void RegisterAdd(object obj);
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <param name="obj"></param>
        bool TryRegisterAdd(object obj);
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <param name="obj"></param>
        void RegisterEdit(object obj);
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <param name="obj"></param>
        bool TryRegisterEdit(object obj);
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <param name="obj"></param>
        void RegisterDelete(object obj);
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <param name="obj"></param>
        bool TryRegisterDelete(object obj);
        /// <summary>
        /// 获得仓储
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>()
            where TRepository : IRepository;
    }
}
