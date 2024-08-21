namespace Materal.TTA.MySqlEFRepository
{
    /// <summary>
    /// MySqlEF工作单元
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MySqlEFUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T>
        where T : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceProvider"></param>
        protected MySqlEFUnitOfWorkImpl(T context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}
