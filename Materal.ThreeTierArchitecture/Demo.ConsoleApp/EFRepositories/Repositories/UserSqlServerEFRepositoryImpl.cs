using System;
using Demo.ConsoleApp.Domain;
using Demo.ConsoleApp.Repositories;

namespace Demo.ConsoleApp.EFRepositories.Repositories
{
    public class UserSqlServerEFRepositoryImpl : DemoSqlServerEFRepositoryImpl<User, Guid>, IUserRepository
    {
        public UserSqlServerEFRepositoryImpl(UserDbContext dbContext) : base(dbContext)
        {
        }
    }
}
