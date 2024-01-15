namespace Materal.BaseCore.EFRepository
{
    /// <summary>
    /// 工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MateralCoreUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T, Guid>, IMateralCoreUnitOfWork
        where T : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public MateralCoreUnitOfWorkImpl(T context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
        public override void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
        {
            if(obj is IDomain domain)
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
