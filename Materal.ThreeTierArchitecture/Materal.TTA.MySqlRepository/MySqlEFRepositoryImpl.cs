using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.MySqlRepository
{
    public class MySqlEFRepositoryImpl<T, TPrimaryKeyType>: EFRepositoryImpl<T,TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
        public MySqlEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
