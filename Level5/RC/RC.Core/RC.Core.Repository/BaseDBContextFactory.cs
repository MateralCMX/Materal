using Microsoft.EntityFrameworkCore.Design;

namespace RC.Core.Repository
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public abstract class BaseDBContextFactory<TDBContext> : IDesignTimeDbContextFactory<TDBContext>
        where TDBContext : DbContext
    {
        /// <summary>
        /// 创建数据库上下文
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public TDBContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TDBContext> optionsBuilder = new();
            SqliteConfigModel sqliteConfigModel = GetSqliteConfigModel();
            optionsBuilder.UseSqlite(sqliteConfigModel.ConnectionString);
            return typeof(TDBContext).Instantiation<TDBContext>(optionsBuilder.Options);
        }
        /// <summary>
        /// 获取Sqlite配置模型
        /// </summary>
        /// <returns></returns>
        protected virtual SqliteConfigModel GetSqliteConfigModel() => new() { Source = DBPath };
        /// <summary>
        /// 数据库路径
        /// </summary>
        protected abstract string DBPath { get; }
    }
}
