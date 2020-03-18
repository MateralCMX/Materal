using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using System;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository
{
    public class ProtalServerSqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : class, IEntity<Guid>, new()
    {
        public ProtalServerSqliteEFRepositoryImpl(ProtalServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
