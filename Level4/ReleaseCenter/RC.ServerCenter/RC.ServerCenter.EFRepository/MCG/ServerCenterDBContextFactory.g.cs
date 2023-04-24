using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RC.ServerCenter.Common;

namespace RC.ServerCenter.EFRepository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public partial class ServerCenterDBContextFactory : IDesignTimeDbContextFactory<ServerCenterDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ServerCenterDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ServerCenterDBContext> optionsBuilder = new();
            optionsBuilder.UseSqlite(ApplicationConfig.DBConfig.ConnectionString);
            return new ServerCenterDBContext(optionsBuilder.Options);
        }
    }
}
