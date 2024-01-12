namespace RC.EnvironmentServer.Repository
{
    /// <summary>
    /// EnvironmentServer工作单元
    /// </summary>
    public partial class EnvironmentServerUnitOfWorkImpl(EnvironmentServerDBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<EnvironmentServerDBContext>(context, serviceProvider) { }
}
