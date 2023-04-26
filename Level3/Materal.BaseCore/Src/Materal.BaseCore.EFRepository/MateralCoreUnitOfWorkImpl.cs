using Materal.BaseCore.Domain;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.BaseCore.EFRepository
{
    /// <summary>
    /// 发布中心工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MateralCoreUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T>, IMateralCoreUnitOfWork, IUnitOfWork
        where T : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public MateralCoreUnitOfWorkImpl(T context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>, IDomain
        {
            obj.CreateTime = DateTime.Now;
            RegisterAdd<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public bool TryRegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>, IDomain
        {
            DateTime temp = obj.CreateTime;
            obj.CreateTime = DateTime.Now;
            return TryRegisterAdd<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>, IDomain
        {
            obj.UpdateTime = DateTime.Now;
            RegisterEdit<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public bool TryRegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>, IDomain
        {
            obj.UpdateTime = DateTime.Now;
            return TryRegisterEdit<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>, IDomain
        {
            RegisterDelete<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public bool TryRegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>, IDomain
        {
            return TryRegisterDelete<TEntity, Guid>(obj);
        }
    }
}
