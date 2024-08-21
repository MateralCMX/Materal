namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// SqlServeerEF工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SqlServerEFUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T>, IUnitOfWork
        where T : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceProvider"></param>
        protected SqlServerEFUnitOfWorkImpl(T context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}
