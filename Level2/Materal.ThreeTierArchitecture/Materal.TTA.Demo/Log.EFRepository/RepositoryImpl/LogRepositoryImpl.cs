using Log.Domain.Repositories;

namespace Log.EFRepository.RepositoryImpl
{
    public class LogRepositoryImpl : LogEFRepositoryImpl<Domain.Log, int>, ILogRepository
    {
        public LogRepositoryImpl(LogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
