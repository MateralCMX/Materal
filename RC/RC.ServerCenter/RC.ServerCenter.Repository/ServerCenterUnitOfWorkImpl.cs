namespace RC.ServerCenter.Repository
{
    /// <summary>
    /// ServerCenter工作单元实现
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class ServerCenterUnitOfWorkImpl(ServerCenterDBContext context, IServiceProvider serviceProvider) : RCUnitOfWorkImpl<ServerCenterDBContext>(context, serviceProvider), IServerCenterUnitOfWork, IScopedDependency<IServerCenterUnitOfWork>
    {
    }
}