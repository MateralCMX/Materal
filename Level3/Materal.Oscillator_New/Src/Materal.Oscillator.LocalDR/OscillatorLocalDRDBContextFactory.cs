using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.Oscillator.LocalDR
{
    /// <summary>
    /// 本地容灾数据库上下文工厂
    /// </summary>
    public class OscillatorLocalDRDBContextFactory : IDesignTimeDbContextFactory<OscillatorLocalDRDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OscillatorLocalDRDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OscillatorLocalDRDBContext>();
            SqliteConfigModel config = new()
            {
                Source = "OscillatorDR.db"
            };
            optionsBuilder.UseSqlite(config.ConnectionString);
            return new OscillatorLocalDRDBContext(optionsBuilder.Options);

        }
    }
}
