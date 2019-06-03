using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// API权限仓储
    /// </summary>
    public class APIAuthorityRepositoryImpl : AuthorityEFRepositoryImpl<APIAuthority, Guid>, IAPIAuthorityRepository
    {
        public APIAuthorityRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
