using Materal.ConfigCenter.ProtalServer.Common;
using Materal.ConfigCenter.ProtalServer.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.ConfigCenter.ProtalServer
{
    public class ProtalServerDBContextFactory : IDesignTimeDbContextFactory<ProtalServerDBContext>
    {
        public ProtalServerDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProtalServerDBContext>();
            optionsBuilder.UseSqlite(ApplicationConfig.SqliteConfig.ConnectionString);
            return new ProtalServerDBContext(optionsBuilder.Options);
        }
    }
}
