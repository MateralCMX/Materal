using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.Abstractions
{
    public interface IUnitOfWork: IDisposable
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
        void RegisterAdd<T>(T obj)
            where T : class, IBaseDomain;
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterEdit<T>(T obj)
            where T : class, IBaseDomain;
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterDelete<T>(T obj)
            where T : class, IBaseDomain;
        /// <summary>
        /// 获得仓储
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>()
            where TRepository : IBaseRepository;
    }
}
