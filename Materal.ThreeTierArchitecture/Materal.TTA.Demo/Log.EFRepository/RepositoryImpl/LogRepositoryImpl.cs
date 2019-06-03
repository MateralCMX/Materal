using Log.Domain.Repositorys;

namespace Log.EFRepository.RepositoryImpl
{
    public class LogRepositoryImpl : LogEFRepositoryImpl<Domain.Log, int>, ILogRepository
    {
        public LogRepositoryImpl(LogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
