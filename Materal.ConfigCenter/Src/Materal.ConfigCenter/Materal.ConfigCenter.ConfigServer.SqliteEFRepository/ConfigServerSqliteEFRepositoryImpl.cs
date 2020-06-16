using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using System;

namespace Materal.ConfigCenter.ConfigServer.SqliteEFRepository
{
    public class ConfigServerSqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : class, IEntity<Guid>, new()
    {
        public ConfigServerSqliteEFRepositoryImpl(ConfigServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
