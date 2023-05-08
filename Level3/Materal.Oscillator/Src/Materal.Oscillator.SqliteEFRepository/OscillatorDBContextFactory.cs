using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.Oscillator.SqliteEFRepository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class OscillatorDBContextFactory : IDesignTimeDbContextFactory<OscillatorSqliteDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OscillatorSqliteDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OscillatorSqliteDBContext>();
            SqliteConfigModel config = new()
            {
                Source = "Oscillator.db"
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new OscillatorSqliteDBContext(optionsBuilder.Options);

        }
    }
}
