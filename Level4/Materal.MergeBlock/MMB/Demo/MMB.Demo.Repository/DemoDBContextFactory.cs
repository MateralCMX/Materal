using Microsoft.EntityFrameworkCore.Design;

namespace MMB.Demo.EFRepository
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
            optionsBuilder.UseSqlite(new SqlServerConfigModel
            {

            }.ConnectionString);
            return new DemoDBContext(optionsBuilder.Options);
        }
    }
}
