namespace RC.ServerCenter.Repository
{
    /// <summary>
    /// ServerCenter工作单元
    /// </summary>
    public partial class ServerCenterUnitOfWorkImpl(ServerCenterDBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<ServerCenterDBContext>(context, serviceProvider) { }
}
