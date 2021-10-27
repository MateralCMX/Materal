using Materal.TTA.EFRepository;
using System;

namespace Deploy.Domain.Repositories
{
    public interface IDefaultDataRepository : ICacheEFRepository<DefaultData, Guid>
    {
    }
}
