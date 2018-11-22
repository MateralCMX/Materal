using Materal.TTA.SqliteRepository;

namespace Materal.ApplicationUpdate.EFRepository
{
    public class AppUpdateUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<AppUpdateContext>, IAppUpdateUnitOfWork
    {
        public AppUpdateUnitOfWorkImpl(AppUpdateContext context) : base(context)
        {
        }
    }
}
