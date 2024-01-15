using Microsoft.EntityFrameworkCore.Design;

namespace Materal.Oscillator.SqlServerEFRepository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class OscillatorDBContextFactory : IDesignTimeDbContextFactory<OscillatorDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OscillatorDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OscillatorDBContext>();
            SqlServerConfigModel config = new()
            {
                Address = "127.0.0.1",
                Port = "1433",
                Name = "OscillatorTestDB",
                UserID = "sa",
                Password = "Materal@1234",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(config.ConnectionString);
            return new OscillatorDBContext(optionsBuilder.Options);

        }
    }
}
