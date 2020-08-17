using Materal.ConDep.Center.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.ConDep.Center.SqliteEFRepository
{
    public class CenterDBContextFactory : IDesignTimeDbContextFactory<CenterDBContext>
    {
        public CenterDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CenterDBContext>();
            optionsBuilder.UseSqlite(ApplicationConfig.SqliteConfig.ConnectionString);
            return new CenterDBContext(optionsBuilder.Options);
        }
    }
}
