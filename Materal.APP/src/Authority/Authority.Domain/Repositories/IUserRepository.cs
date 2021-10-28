using System;
using Materal.TTA.EFRepository;

namespace Authority.Domain.Repositories
{
    public interface IUserRepository : IEFRepository<User, Guid>
    {
    }
}
