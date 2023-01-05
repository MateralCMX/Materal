using Materal.CacheHelper;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace RC.Core.SqlServer
{
    public abstract class RCCacheRepositoryImpl<T, TPrimaryKeyType> : CacheEFRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        public RCCacheRepositoryImpl(DbContext dbContext, ICacheManager cacheManager) : base(dbContext, cacheManager, null)
        {
        }
    }
}
