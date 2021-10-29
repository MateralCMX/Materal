using Materal.TTA.EFRepository;
using System;

namespace ConfigCenter.Domain.Repositories
{
    public interface INamespaceRepository : IEFRepository<Namespace, Guid>
    {
    }
}
