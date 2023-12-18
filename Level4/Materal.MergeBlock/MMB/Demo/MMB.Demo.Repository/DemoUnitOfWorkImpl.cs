namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo工作单元
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class DemoUnitOfWorkImpl(DemoDBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<DemoDBContext>(context, serviceProvider)
    {
    }
}
