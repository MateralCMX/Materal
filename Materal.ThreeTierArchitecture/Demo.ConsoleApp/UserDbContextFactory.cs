using Demo.ConsoleApp.EFRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Demo.ConsoleApp
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<UserDbContext>();
            options.UseSqlServer(ApplicationConfig.UserDB.ConnectionString, m =>
            {
                m.EnableRetryOnFailure(); 
            });
            return new UserDbContext(options.Options);
        }
    }
}
