using Materal.MergeBlock.Domain;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.MergeBlock.Repository
{
    /// <summary>
    /// 工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MateralCoreUnitOfWorkImpl<T>(T context, IServiceProvider serviceProvider) : EFUnitOfWorkImpl<T, Guid>(context, serviceProvider), IMateralCoreUnitOfWork
        where T : DbContext
    {
        public override void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
        {
            if (obj is IDomain domain)
            {
                domain.CreateTime = DateTime.Now;
            }
            base.RegisterAdd<TEntity, TPrimaryKeyType>(obj);
        }
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
