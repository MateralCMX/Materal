using System;
using Materal.TTA.EFRepository;

namespace Materal.ConDep.Center.Domain.Repositories
{
    public interface IUserRepository : IEFRepository<User, Guid>
    {
    }
}
