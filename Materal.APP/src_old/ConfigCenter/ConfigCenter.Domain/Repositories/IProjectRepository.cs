using Materal.TTA.EFRepository;
using System;

namespace ConfigCenter.Domain.Repositories
{
    public interface IProjectRepository : IEFRepository<Project, Guid>
    {
    }
}
