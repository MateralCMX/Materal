namespace Materal.TTA.SqliteEFRepository
{
    /// <summary>
    /// SqliteEF工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SqliteEFUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T>
        where T : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceProvider"></param>
        protected SqliteEFUnitOfWorkImpl(T context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}
