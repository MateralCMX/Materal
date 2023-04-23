using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerRepository
{
    public abstract class SqlServerEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : EFRepositoryImpl<T, TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        protected SqlServerEFRepositoryImpl(TDBContext dbContext) : base(dbContext)
        {
        }
    }
}
