namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo工作单元
    /// </summary>
    public class DemoUnitOfWorkImpl(DemoDBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<DemoDBContext>(context, serviceProvider) { }
}
