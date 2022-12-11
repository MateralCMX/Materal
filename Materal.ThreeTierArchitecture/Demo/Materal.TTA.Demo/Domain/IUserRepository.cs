using Materal.TTA.EFRepository;

namespace Materal.TTA.Demo.Domain
{
    public interface IUserRepository : IEFRepository<User, Guid>
    {
    }
}
