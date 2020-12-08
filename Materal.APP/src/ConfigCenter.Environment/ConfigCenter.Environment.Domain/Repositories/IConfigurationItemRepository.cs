using System;
using Materal.TTA.EFRepository;

namespace ConfigCenter.Environment.Domain.Repositories
{
    public interface IConfigurationItemRepository : ICacheEFRepository<ConfigurationItem, Guid>
    {
    }
}
