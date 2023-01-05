using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RC.Authority.RepositoryImpl;

namespace RC.Authority
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class AuthorityDBContextFactory : IDesignTimeDbContextFactory<AuthorityDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public AuthorityDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthorityDBContext>();
            var config = new SqliteConfigModel
            {
                Source = "./Authority.db"
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new AuthorityDBContext(optionsBuilder.Options);
        }
    }
}
