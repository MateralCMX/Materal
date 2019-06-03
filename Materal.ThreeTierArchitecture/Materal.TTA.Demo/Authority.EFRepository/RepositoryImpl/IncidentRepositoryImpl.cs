using System;
using Authority.Domain;
using Authority.Domain.Repositories;
namespace Authority.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 事件仓储
    /// </summary>
    public class IncidentRepositoryImpl : AuthorityEFRepositoryImpl<Incident, Guid>, IIncidentRepository
    {
        public IncidentRepositoryImpl(AuthorityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
