using Materal.BaseCore.EFRepository;

namespace RC.Deploy.EFRepository
{
    public class DeployUnitOfWorkImpl : MateralCoreUnitOfWorkImpl<DeployDBContext>
    {
        public DeployUnitOfWorkImpl(DeployDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider) { }
    }
}
