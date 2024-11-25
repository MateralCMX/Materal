using Materal.MergeBlock.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using MMB.Demo.Abstractions;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// MMB工作单元实现
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class MMBUnitOfWorkImpl<T>(T context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<T>(context, serviceProvider), IMMBUnitOfWork
        where T : DbContext
    {
    }
}