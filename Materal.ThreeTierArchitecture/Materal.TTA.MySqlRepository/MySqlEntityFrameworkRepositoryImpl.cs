using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.MySqlRepository
{
    public class MySqlEntityFrameworkRepositoryImpl<T, TPrimaryKeyType>: EntityFrameworkRepositoryImpl<T,TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
        public MySqlEntityFrameworkRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
