using Materal.TTA.Common.Model;
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
                Address = "175.27.194.19",
                Port = "1433",
                Name = "TTATestDB",
                UserID = "sa",
                Password = "XMJry@456",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(config.ConnectionString);
            return new TTADemoDBContext(optionsBuilder.Options);

        }
    }
}
