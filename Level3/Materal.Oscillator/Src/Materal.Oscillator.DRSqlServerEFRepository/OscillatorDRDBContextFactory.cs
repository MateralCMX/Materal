using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.Oscillator.DRSqlServerEFRepository
{
    /// <summary>
    /// 容灾数据库上下文工厂
    /// </summary>
    public class OscillatorDRDBContextFactory : IDesignTimeDbContextFactory<OscillatorDRDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OscillatorDRDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OscillatorDRDBContext>();
            SqlServerConfigModel config = new()
            {
                Address = "82.156.11.176",
                Port = "1433",
                Name = "OscillatorDRTestDB",
                UserID = "sa",
                Password = "gdb@admin678",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(config.ConnectionString);
            return new OscillatorDRDBContext(optionsBuilder.Options);

        }
    }
}
