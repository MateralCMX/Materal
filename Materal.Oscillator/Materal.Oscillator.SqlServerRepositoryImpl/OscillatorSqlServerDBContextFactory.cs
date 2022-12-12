using Materal.Oscillator.Abstractions;
using Materal.TTA.SqlServerRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.TTA.Demo
{
    public class OscillatorSqlServerDBContextFactory : IDesignTimeDbContextFactory<OscillatorSqlServerDBContext>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OscillatorSqlServerDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OscillatorSqlServerDBContext>();
            SqlServerConfigModel config = new()
            {
                Address = "175.27.194.19",
                Port = "1433",
                Name = "OscillatorTestDB",
                UserID = "sa",
                Password = "XMJry@456",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(config.ConnectionString);
            return new OscillatorSqlServerDBContext(optionsBuilder.Options);

        }
    }
}
