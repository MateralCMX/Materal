using Materal.APP.Core.Models;
using Materal.TTA.SqliteRepository;
using System;

namespace Authority.SqliteEFRepository
{
    public class AuthoritySqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : BaseDomain, new()
    {
        public AuthoritySqliteEFRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
