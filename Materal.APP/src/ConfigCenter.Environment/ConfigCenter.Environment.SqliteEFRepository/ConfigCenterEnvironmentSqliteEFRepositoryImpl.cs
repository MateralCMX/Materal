using System;
using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;

namespace ConfigCenter.Environment.SqliteEFRepository
{
    public class ConfigCenterEnvironmentSqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : class, IEntity<Guid>, new()
    {
        public ConfigCenterEnvironmentSqliteEFRepositoryImpl(ConfigCenterEnvironmentDBContext dbContext) : base(dbContext)
        {
        }
    }
}
