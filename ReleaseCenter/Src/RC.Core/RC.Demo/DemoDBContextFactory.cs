//using Materal.TTA.SqliteRepository.Model;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using RC.Demo.RepositoryImpl;

//namespace RC.Demo
//{
//    /// <summary>
//    /// 数据库上下文工厂
//    /// </summary>
//    public class DemoDBContextFactory : IDesignTimeDbContextFactory<DemoDBContext>
//    {
//        /// <summary>
//        /// 创建数据库连接
//        /// </summary>
//        /// <param name="args"></param>
//        /// <returns></returns>
//        public DemoDBContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<DemoDBContext>();
//            var config = new SqliteConfigModel
//            {
//                Source = "./Demo.db"
//            };
//            optionsBuilder.UseSqlite(config.ConnectionString);
//            return new DemoDBContext(optionsBuilder.Options);
//        }
//    }
//}
