using Deploy.Common;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Deploy.SqliteEFRepository
{
    public class DeployDBContextFactory : IDesignTimeDbContextFactory<DeployDBContext>
    {
        public DeployDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeployDBContext>();
            var config = new SqliteConfigModel
            {
                FilePath = "DeployDB.db",
                Password = string.Empty,
                Version = string.Empty
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new DeployDBContext(optionsBuilder.Options);
        }
    }
}
