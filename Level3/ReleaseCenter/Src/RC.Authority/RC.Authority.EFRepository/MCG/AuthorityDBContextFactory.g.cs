using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RC.Authority.Common;

namespace RC.Authority.EFRepository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public partial class AuthorityDBContextFactory : IDesignTimeDbContextFactory<AuthorityDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public AuthorityDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AuthorityDBContext> optionsBuilder = new();
            optionsBuilder.UseSqlite(ApplicationConfig.DBConfig.ConnectionString);
            return new AuthorityDBContext(optionsBuilder.Options);
        }
    }
}
