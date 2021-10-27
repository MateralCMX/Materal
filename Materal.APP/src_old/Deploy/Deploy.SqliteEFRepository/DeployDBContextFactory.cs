using Deploy.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Deploy.SqliteEFRepository
{
    public class DeployDBContextFactory : IDesignTimeDbContextFactory<DeployDBContext>
    {
        public DeployDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeployDBContext>();
            optionsBuilder.UseSqlite(DeployConfig.SqliteConfig.ConnectionString);
            return new DeployDBContext(optionsBuilder.Options);
        }
    }
}
