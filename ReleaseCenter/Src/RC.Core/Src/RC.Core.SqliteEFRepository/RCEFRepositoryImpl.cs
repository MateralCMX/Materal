using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using Microsoft.EntityFrameworkCore;

namespace RC.Core.SqlServer
{
    public abstract class RCEFRepositoryImpl<T, TPrimaryKeyType> : SqliteEFRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        protected RCEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
