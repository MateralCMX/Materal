namespace RC.Deploy.Repository
{
    /// <summary>
    /// Deploy工作单元实现
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class DeployUnitOfWorkImpl(DeployDBContext context, IServiceProvider serviceProvider) : RCUnitOfWorkImpl<DeployDBContext>(context, serviceProvider), IDeployUnitOfWork, IScopedDependencyInjectionService<IDeployUnitOfWork>
    {
    }
}