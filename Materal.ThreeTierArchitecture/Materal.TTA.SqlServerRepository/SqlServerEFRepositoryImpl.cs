using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerRepository
{
    public abstract class SqlServerEFRepositoryImpl<T, TPrimaryKeyType>: EFRepositoryImpl<T,TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>
    {
        protected SqlServerEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
