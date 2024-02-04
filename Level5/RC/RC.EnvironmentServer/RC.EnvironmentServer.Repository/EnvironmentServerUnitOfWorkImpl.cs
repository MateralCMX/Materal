namespace RC.EnvironmentServer.Repository
{
    /// <summary>
    /// EnvironmentServer工作单元实现
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class EnvironmentServerUnitOfWorkImpl(EnvironmentServerDBContext context, IServiceProvider serviceProvider) : RCUnitOfWorkImpl<EnvironmentServerDBContext>(context, serviceProvider), IEnvironmentServerUnitOfWork, IScopedDependencyInjectionService<IEnvironmentServerUnitOfWork>
    {
    }
}