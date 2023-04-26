using Materal.BaseCore.Domain;
using Materal.TTA.Common;

namespace Materal.BaseCore.EFRepository
{
    /// <summary>
    /// 核心工作单元
    /// </summary>
    public interface IMateralCoreUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterAdd<T>(T obj)
            where T : class, IEntity<Guid>, IDomain;
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterAdd<T>(T obj)
            where T : class, IEntity<Guid>, IDomain;
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterEdit<T>(T obj)
            where T : class, IEntity<Guid>, IDomain;
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterEdit<T>(T obj)
            where T : class, IEntity<Guid>, IDomain;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterDelete<T>(T obj)
            where T : class, IEntity<Guid>, IDomain;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterDelete<T>(T obj)
            where T : class, IEntity<Guid>, IDomain;
    }
}
