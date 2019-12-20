using System;
using Demo.ConsoleApp.Domain;
using Demo.ConsoleApp.Repositories;

namespace Demo.ConsoleApp.EFRepositories.Repositories
{
    public class UserEFRepositoryImpl : DemoEFRepositoryImpl<User, Guid>, IUserRepository
    {
        public UserEFRepositoryImpl(UserDbContext dbContext) : base(dbContext)
        {
        }
    }
}
