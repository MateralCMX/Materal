using Materal.Oscillator.LocalDR;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.TTA.Demo
{
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
