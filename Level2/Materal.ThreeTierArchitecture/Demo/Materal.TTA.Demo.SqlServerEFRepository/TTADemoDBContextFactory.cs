using Materal.TTA.SqlServerEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.TTA.Demo.SqlServerEFRepository
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
            SqlServerConfigModel config = new()
            {
                Address = "127.0.0.1",
                Port = "1433",
                Name = "TTATestDB",
                UserID = "sa",
                Password = "Materal@1234",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(config.ConnectionString);
            return new TTADemoDBContext(optionsBuilder.Options);

        }
    }
}
