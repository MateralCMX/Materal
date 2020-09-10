using Authority.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Authority.SqliteEFRepository
{
    public class AuthorityDBContextFactory : IDesignTimeDbContextFactory<AuthorityDBContext>
    {
        public AuthorityDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthorityDBContext>();
            optionsBuilder.UseSqlite(AuthorityConfig.SqliteConfig.ConnectionString);
            return new AuthorityDBContext(optionsBuilder.Options);
        }
    }
}
