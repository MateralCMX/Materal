namespace RC.Core.Repository
{
    /// <summary>
    /// RC工作单元实现
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class RCUnitOfWorkImpl<T>(T context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<T>(context, serviceProvider), IRCUnitOfWork
        where T : DbContext
    {
    }
}