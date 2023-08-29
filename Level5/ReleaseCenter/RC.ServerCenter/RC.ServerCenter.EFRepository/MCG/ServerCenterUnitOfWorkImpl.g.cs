using Materal.BaseCore.EFRepository;

namespace RC.ServerCenter.EFRepository
{
    public class ServerCenterUnitOfWorkImpl : MateralCoreUnitOfWorkImpl<ServerCenterDBContext>
    {
        public ServerCenterUnitOfWorkImpl(ServerCenterDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider) { }
    }
}
