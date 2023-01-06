using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RC.Deploy.RepositoryImpl;

namespace RC.Deploy
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class DeployDBContextFactory : IDesignTimeDbContextFactory<DeployDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DeployDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeployDBContext>();
            var config = new SqliteConfigModel
            {
                Source = "./Deploy.db"
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new DeployDBContext(optionsBuilder.Options);
        }
    }
}
