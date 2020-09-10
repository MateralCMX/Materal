using Materal.APP.Core.Models;
using Materal.TTA.SqliteRepository;
using System;

namespace ConfigCenter.SqliteEFRepository
{
    public class ConfigCenterSqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : BaseDomain, new()
    {
        public ConfigCenterSqliteEFRepositoryImpl(ConfigCenterDBContext dbContext) : base(dbContext)
        {
        }
    }
}
