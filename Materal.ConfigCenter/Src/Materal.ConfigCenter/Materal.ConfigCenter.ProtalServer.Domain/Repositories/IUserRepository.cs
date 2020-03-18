using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using System;

namespace Materal.ConfigCenter.ProtalServer.Domain.Repositories
{
    public interface IUserRepository : IEFRepository<User, Guid>
    {
    }
}
