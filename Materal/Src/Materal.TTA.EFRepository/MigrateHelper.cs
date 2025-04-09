namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// 迁移帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MigrateHelper<T> : IMigrateHelper<T>, IDisposable
        where T : DbContext
    {
        private readonly T _dbContext;
        private readonly ILogger<MigrateHelper<T>>? _logger;
        private readonly string _dbName;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public MigrateHelper(T dbContext, ILogger<MigrateHelper<T>>? logger = null)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbName = _dbContext.GetType().Name;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 迁移
        /// </summary>
        /// <returns></returns>
        public async Task MigrateAsync()
        {
            IEnumerable<string> migrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (migrations.Any())
            {
                _logger?.LogInformation($"正在迁移数据库[{_dbName}]...");
                try
                {
                    await _dbContext.Database.MigrateAsync();
                    _logger?.LogInformation($"数据库[{_dbName}]迁移完毕");
                }
                catch (Exception exception)
                {
                    _logger?.LogError(exception, $"数据库[{_dbName}]迁移失败.");
                }
            }
            else
            {
                _logger?.LogInformation($"数据库[{_dbName}]无需迁移.");
            }
        }
        /// <summary>
        /// 迁移
        /// </summary>
        /// <returns></returns>
        public void Migrate()
        {
            IEnumerable<string> migrations = _dbContext.Database.GetPendingMigrations();
            if (migrations.Any())
            {
                _logger?.LogInformation($"正在迁移数据库[{_dbName}]...");
                try
                {
                    _dbContext.Database.Migrate();
                    _logger?.LogInformation($"数据库[{_dbName}]迁移完毕");
                }
                catch (Exception exception)
                {
                    _logger?.LogError(exception, $"数据库[{_dbName}]迁移失败.");
                }
            }
            else
            {
                _logger?.LogInformation($"数据库[{_dbName}]无需迁移.");
            }
        }
    }
}
