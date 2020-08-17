using System;
using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace Materal.ConDep.Center.SqliteEFRepository
{
    public class CenterSqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : class, IEntity<Guid>, new()
    {
        public CenterSqliteEFRepositoryImpl(CenterDBContext dbContext) : base(dbContext)
        {
        }
    }
}
