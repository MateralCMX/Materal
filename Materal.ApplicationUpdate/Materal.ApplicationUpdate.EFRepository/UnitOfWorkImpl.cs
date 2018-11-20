using Materal.TTA.SqliteRepository;

namespace Materal.ApplicationUpdate.EFRepository
{
    public class UnitOfWorkImpl : SqliteEFUnitOfWorkImpl<AppUpdateContext>
    {
        public UnitOfWorkImpl(AppUpdateContext context) : base(context)
        {
        }
    }
}
