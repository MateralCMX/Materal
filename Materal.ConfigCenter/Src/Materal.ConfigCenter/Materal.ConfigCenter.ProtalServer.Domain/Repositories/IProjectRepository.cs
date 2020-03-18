using System;
using Materal.TTA.EFRepository;

namespace Materal.ConfigCenter.ProtalServer.Domain.Repositories
{
    public interface IProjectRepository : IEFRepository<Project, Guid>
    {
    }
}
