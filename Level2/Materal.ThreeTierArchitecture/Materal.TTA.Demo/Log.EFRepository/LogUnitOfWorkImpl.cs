using Materal.TTA.Common;

namespace Log.EFRepository
{
    public class LogUnitOfWorkImpl : EFUnitOfWorkImpl<LogDbContext>, ILogUnitOfWork
    {
        public LogUnitOfWorkImpl(LogDbContext context) : base(context)
        {

        }
    }
}
