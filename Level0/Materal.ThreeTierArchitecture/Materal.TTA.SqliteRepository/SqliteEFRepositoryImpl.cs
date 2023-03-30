using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqliteRepository
{
    public abstract class SqliteEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : EFRepositoryImpl<T, TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
    }
}
