using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RC.Demo.Common;

namespace RC.Demo.EFRepository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public partial class DemoDBContextFactory : IDesignTimeDbContextFactory<DemoDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DemoDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DemoDBContext> optionsBuilder = new();
            optionsBuilder.UseSqlite(ApplicationConfig.DBConfig.ConnectionString);
            return new DemoDBContext(optionsBuilder.Options);
        }
    }
}
