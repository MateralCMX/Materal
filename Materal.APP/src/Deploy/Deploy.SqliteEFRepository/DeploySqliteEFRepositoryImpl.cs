using Materal.APP.Core.Models;
using Materal.TTA.SqliteRepository;
using System;

namespace Deploy.SqliteEFRepository
{
    public class DeploySqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : BaseDomain, new()
    {
        public DeploySqliteEFRepositoryImpl(DeployDBContext dbContext) : base(dbContext)
        {
        }
    }
}
