namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo������Ԫ
    /// </summary>
    public class DemoUnitOfWorkImpl(DemoDBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<DemoDBContext>(context, serviceProvider) { }
}
