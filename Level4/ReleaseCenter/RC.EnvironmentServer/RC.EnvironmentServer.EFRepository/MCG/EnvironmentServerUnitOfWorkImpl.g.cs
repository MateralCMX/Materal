using Materal.BaseCore.EFRepository;

namespace RC.EnvironmentServer.EFRepository
{
    public class EnvironmentServerUnitOfWorkImpl : MateralCoreUnitOfWorkImpl<EnvironmentServerDBContext>
    {
        public EnvironmentServerUnitOfWorkImpl(EnvironmentServerDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider) { }
    }
}
