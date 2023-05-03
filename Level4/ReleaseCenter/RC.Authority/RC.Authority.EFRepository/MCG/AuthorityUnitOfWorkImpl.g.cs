using Materal.BaseCore.EFRepository;

namespace RC.Authority.EFRepository
{
    public class AuthorityUnitOfWorkImpl : MateralCoreUnitOfWorkImpl<AuthorityDBContext>
    {
        public AuthorityUnitOfWorkImpl(AuthorityDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider) { }
    }
}
