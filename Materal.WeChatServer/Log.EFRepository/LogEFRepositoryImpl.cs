using Materal.TTA.Common;

namespace Log.EFRepository
{
    public class LogEFRepositoryImpl<T,TKey> : EFRepositoryImpl<T, TKey> where T : class, IEntity<TKey>, new()
    {
        public LogEFRepositoryImpl(LogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
