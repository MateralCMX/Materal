using Materal.BaseCore.EFRepository;

namespace RC.Demo.EFRepository
{
    public class DemoUnitOfWorkImpl : MateralCoreUnitOfWorkImpl<DemoDBContext>
    {
        public DemoUnitOfWorkImpl(DemoDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider) { }
    }
}
