using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.MySqlRepository
{
    public abstract class MySqlEFRepositoryImpl<T, TPrimaryKeyType>: EFRepositoryImpl<T,TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
        protected MySqlEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
