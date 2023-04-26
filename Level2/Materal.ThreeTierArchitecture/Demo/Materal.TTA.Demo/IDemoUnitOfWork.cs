using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace Materal.TTA.Demo
{
    public interface IDemoUnitOfWork : IEFUnitOfWork
    {
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterAdd<T>(T obj)
            where T : class, IEntity<Guid>;
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterAdd<T>(T obj)
            where T : class, IEntity<Guid>;
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterEdit<T>(T obj)
            where T : class, IEntity<Guid>;
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterEdit<T>(T obj)
            where T : class, IEntity<Guid>;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterDelete<T>(T obj)
            where T : class, IEntity<Guid>;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        bool TryRegisterDelete<T>(T obj)
            where T : class, IEntity<Guid>;
    }
}
