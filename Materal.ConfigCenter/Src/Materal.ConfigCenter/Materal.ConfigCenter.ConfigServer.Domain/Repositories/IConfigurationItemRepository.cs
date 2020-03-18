using Materal.TTA.EFRepository;
using System;

namespace Materal.ConfigCenter.ConfigServer.Domain.Repositories
{
    public interface IConfigurationItemRepository : IEFRepository<ConfigurationItem, Guid>
    {
    }
}
