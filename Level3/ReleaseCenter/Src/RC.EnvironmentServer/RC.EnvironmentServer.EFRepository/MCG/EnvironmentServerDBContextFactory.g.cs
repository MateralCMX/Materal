using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RC.EnvironmentServer.Common;

namespace RC.EnvironmentServer.EFRepository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public partial class EnvironmentServerDBContextFactory : IDesignTimeDbContextFactory<EnvironmentServerDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public EnvironmentServerDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<EnvironmentServerDBContext> optionsBuilder = new();
            optionsBuilder.UseSqlite(ApplicationConfig.DBConfig.ConnectionString);
            return new EnvironmentServerDBContext(optionsBuilder.Options);
        }
    }
}
