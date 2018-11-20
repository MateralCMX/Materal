using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqliteRepository
{
    public class SqliteEFRepositoryImpl<T, TPrimaryKeyType>: EFRepositoryImpl<T,TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
        public SqliteEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
