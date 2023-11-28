using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Materal.TTA.Demo.MySqlEFRepository
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
            MySqlConfigModel config = new()
            {
                Address = "127.0.0.1",
                Port = "3306",
                Name = "TTATestDB",
                UserID = "root",
                Password = "Materal@1234",
            };
            optionsBuilder.UseMySQL(config.ConnectionString);
            return new TTADemoDBContext(optionsBuilder.Options);
        }
    }
}
