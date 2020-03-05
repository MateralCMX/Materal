using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqliteRepository
{
    public abstract class SqliteEFRepositoryImpl<T, TPrimaryKeyType>: EFRepositoryImpl<T,TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
        protected SqliteEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
