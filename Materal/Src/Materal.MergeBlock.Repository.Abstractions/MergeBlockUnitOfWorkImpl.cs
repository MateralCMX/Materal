namespace Materal.MergeBlock.Repository.Abstractions
{
    /// <summary>
    /// 工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MergeBlockUnitOfWorkImpl<T>(T context, IServiceProvider serviceProvider) : EFUnitOfWorkImpl<T>(context, serviceProvider), IMergeBlockUnitOfWork
        where T : DbContext
    {
        /// <inheritdoc/>
        public override void RegisterAdd(object obj)
        {
            if (obj is IDomain domain)
            {
                domain.CreateTime = DateTime.Now;
            }
            base.RegisterAdd(obj);
        }
        /// <inheritdoc/>
        public override void RegisterEdit(object obj)
        {
            if (obj is IDomain domain)
            {
                domain.UpdateTime = DateTime.Now;
            }
            base.RegisterEdit(obj);
        }
    }
}
