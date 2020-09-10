using ConfigCenter.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConfigCenter.SqliteEFRepository
{
    public class ConfigCenterDBContextFactory : IDesignTimeDbContextFactory<ConfigCenterDBContext>
    {
        public ConfigCenterDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigCenterDBContext>();
            optionsBuilder.UseSqlite(ConfigCenterConfig.SqliteConfig.ConnectionString);
            return new ConfigCenterDBContext(optionsBuilder.Options);
        }
    }
}