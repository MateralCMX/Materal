using Demo.ConsoleApp.Domain;
using Materal.TTA.EFRepository;
using System;

namespace Demo.ConsoleApp.Repositories
{
    public interface IUserRepository : IEFSubordinateRepository<User, Guid>
    {
    }
}
