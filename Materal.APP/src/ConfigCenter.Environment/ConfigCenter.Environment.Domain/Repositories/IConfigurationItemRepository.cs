using System;
using Materal.TTA.EFRepository;

namespace ConfigCenter.Environment.Domain.Repositories
{
    public interface IConfigurationItemRepository : IEFRepository<ConfigurationItem, Guid>
    {
    }
}
