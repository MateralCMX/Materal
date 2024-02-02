namespace RC.Deploy.Repository
{
    /// <summary>
    /// Deploy工作单元
    /// </summary>
    public partial class DeployUnitOfWorkImpl(DeployDBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<DeployDBContext>(context, serviceProvider) { }
}
