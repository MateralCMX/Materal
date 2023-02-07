using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RC.Deploy.Common;

namespace RC.Deploy.EFRepository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public partial class DeployDBContextFactory : IDesignTimeDbContextFactory<DeployDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DeployDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DeployDBContext> optionsBuilder = new();
            optionsBuilder.UseSqlite(ApplicationConfig.DBConfig.ConnectionString);
            return new DeployDBContext(optionsBuilder.Options);
        }
    }
}
