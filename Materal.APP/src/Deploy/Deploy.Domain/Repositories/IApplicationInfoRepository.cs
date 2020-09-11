using System;
using Materal.TTA.EFRepository;

namespace Deploy.Domain.Repositories
{
    public interface IApplicationInfoRepository : IEFRepository<ApplicationInfo, Guid>
    {
    }
}
