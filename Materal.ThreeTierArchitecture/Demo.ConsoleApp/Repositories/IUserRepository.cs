using Demo.ConsoleApp.Domain;
using Materal.TTA.Common;
using System;

namespace Demo.ConsoleApp.Repositories
{
    public interface IUserRepository : IEFSubordinateRepository<User, Guid>
    {
    }
}
