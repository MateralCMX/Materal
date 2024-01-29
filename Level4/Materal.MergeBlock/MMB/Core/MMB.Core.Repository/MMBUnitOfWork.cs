namespace MMB.Core.Repository
{
    /// <summary>
    /// MMB工作单元实现
    /// </summary>
    /// <typeparam name="TDBContext"></typeparam>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class MMBUnitOfWorkImpl<TDBContext>(TDBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<TDBContext>(context, serviceProvider), IMMBUnitOfWork
        where TDBContext : DbContext
    {
    }
}
