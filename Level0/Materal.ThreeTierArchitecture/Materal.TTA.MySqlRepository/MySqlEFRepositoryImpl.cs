using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.MySqlRepository
{
    public abstract class MySqlEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : EFRepositoryImpl<T,TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
    }
}
