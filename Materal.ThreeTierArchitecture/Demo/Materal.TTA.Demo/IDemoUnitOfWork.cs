using Materal.TTA.Common;

namespace Materal.TTA.Demo
{
    public interface IDemoUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterAdd<T>(T obj)
            where T : class, IEntity<Guid>;
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterEdit<T>(T obj)
            where T : class, IEntity<Guid>;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void RegisterDelete<T>(T obj)
            where T : class, IEntity<Guid>;
    }
}
