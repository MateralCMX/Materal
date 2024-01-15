using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.TTA.Demo.SqliteEFRepository
{
    public class TTADemoDBContextFactory : IDesignTimeDbContextFactory<TTADemoDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public TTADemoDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TTADemoDBContext>();
            SqliteConfigModel config = new();
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new TTADemoDBContext(optionsBuilder.Options);

        }
    }
}
