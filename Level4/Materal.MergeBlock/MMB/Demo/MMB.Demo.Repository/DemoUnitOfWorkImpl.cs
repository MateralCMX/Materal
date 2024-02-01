using MMB.Demo.Abstractions;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo工作单元实现
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class DemoUnitOfWorkImpl(DemoDBContext context, IServiceProvider serviceProvider) : MMBUnitOfWorkImpl<DemoDBContext>(context, serviceProvider), IDemoUnitOfWork, IScopedDependencyInjectionService<IDemoUnitOfWork>
    {
    }
}
