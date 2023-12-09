namespace MMB.Demo.Repository
{
    public class DemoUnitOfWorkImpl(DemoDBContext context, IServiceProvider serviceProvider) : MateralCoreUnitOfWorkImpl<DemoDBContext>(context, serviceProvider)
    {
    }
}
