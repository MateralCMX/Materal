using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using Microsoft.EntityFrameworkCore;
using RC.Core.SqliteEFRepository;

namespace RC.Core.SqlServer
{
    /// <summary>
    /// 发布中心工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RCUnitOfWorkImpl<T> : SqliteEFUnitOfWorkImpl<T>, IRCUnitOfWork, IUnitOfWork
        where T : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RCUnitOfWorkImpl(T context) : base(context)
        {
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterAdd<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterDelete<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterEdit<TEntity, Guid>(obj);
        }
    }
}
