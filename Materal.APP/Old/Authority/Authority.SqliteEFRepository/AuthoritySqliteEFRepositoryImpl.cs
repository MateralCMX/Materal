using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using System;

namespace Authority.SqliteEFRepository
{
    public class AuthoritySqliteEFRepositoryImpl<T> : SqliteEFRepositoryImpl<T, Guid> where T : class, IEntity<Guid>, new()
    {
        public AuthoritySqliteEFRepositoryImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
