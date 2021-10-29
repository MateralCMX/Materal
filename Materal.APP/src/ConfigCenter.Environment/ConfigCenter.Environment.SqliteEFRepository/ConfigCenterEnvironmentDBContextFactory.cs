using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConfigCenter.Environment.SqliteEFRepository
{
    public class ConfigCenterEnvironmentDBContextFactory : IDesignTimeDbContextFactory<ConfigCenterEnvironmentDBContext>
    {
        public ConfigCenterEnvironmentDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigCenterEnvironmentDBContext>();
            var config = new SqliteConfigModel
            {
                FilePath = "ConfigCenterEnvironmentDB.db",
                Password = string.Empty,
                Version = string.Empty
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new ConfigCenterEnvironmentDBContext(optionsBuilder.Options);
        }
    }
}
