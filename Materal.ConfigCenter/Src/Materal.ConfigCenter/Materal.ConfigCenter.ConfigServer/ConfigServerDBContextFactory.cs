using Materal.ConfigCenter.ConfigServer.Common;
using Materal.ConfigCenter.ConfigServer.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.ConfigCenter.ConfigServer
{
    public class ConfigServerDBContextFactory : IDesignTimeDbContextFactory<ConfigServerDBContext>
    {
        public ConfigServerDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigServerDBContext>();
            optionsBuilder.UseSqlite(ApplicationConfig.SqliteConfig.ConnectionString);
            return new ConfigServerDBContext(optionsBuilder.Options);
        }
    }
}
