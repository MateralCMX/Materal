using Materal.MergeBlock.Domain;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.MergeBlock.Repository
{
    /// <summary>
    /// 工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MergeBlockUnitOfWorkImpl<T>(T context, IServiceProvider serviceProvider) : EFUnitOfWorkImpl<T, Guid>(context, serviceProvider), IMergeBlockUnitOfWork
        where T : DbContext
    {
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        public override void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
        {
            if (obj is IDomain domain)
            {
                domain.CreateTime = DateTime.Now;
            }
            base.RegisterAdd<TEntity, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册编辑
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        public override void RegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
        {
            if (obj is IDomain domain)
            {
                domain.UpdateTime = DateTime.Now;
            }
            base.RegisterEdit<TEntity, TPrimaryKeyType>(obj);
        }
    }
}
