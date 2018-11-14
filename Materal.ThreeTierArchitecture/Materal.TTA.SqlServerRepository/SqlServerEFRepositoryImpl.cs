using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerRepository
{
    public class SqlServerEFRepositoryImpl<T, TPrimaryKeyType>: EFRepositoryImpl<T,TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
        public SqlServerEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
