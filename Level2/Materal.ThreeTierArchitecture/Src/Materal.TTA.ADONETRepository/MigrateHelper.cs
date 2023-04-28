using Microsoft.Extensions.Logging;
using System.Data;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 迁移帮助类
    /// </summary>
    /// <typeparam name="TDBOption"></typeparam>
    public class MigrateHelper<TDBOption> : IMigrateHelper<TDBOption>
        where TDBOption : DBOption
    {
        private readonly TDBOption _dbOption;
        private readonly ILogger<MigrateHelper<TDBOption>>? _logger;
        private readonly IMaigrateRepository _maigrateRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbOption"></param>
        /// <param name="logger"></param>
        public MigrateHelper(TDBOption dbOption, ILogger<MigrateHelper<TDBOption>>? logger = null, IMaigrateRepository maigrateRepository = null)
        {
            _dbOption = dbOption;
            _logger = logger;
            _maigrateRepository = maigrateRepository;
        }
        /// <summary>
        /// 迁移
        /// </summary>
        /// <returns></returns>
        public Task MigrateAsync()
        {
            Migrate();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 迁移
        /// </summary>
        /// <returns></returns>
        public void Migrate()
        {
            List<Migration> migrations = GetPendingMigrations();
            if (migrations.Any())
            {
                _logger?.LogInformation("正在迁移数据库...");
                try
                {
                    migrations = migrations.OrderBy(m => m.Index).ToList();
                    using IDbConnection dbConnection = _dbOption.GetConnection();
                    dbConnection.Open();
                    foreach (Migration migration in migrations)
                    {
                        migration.Migrate(dbConnection);
                    }
                    dbConnection.Close();
                    _logger?.LogInformation("数据库迁移完毕");
                }
                catch (Exception exception)
                {
                    _logger?.LogError(exception, "数据库迁移失败.");
                }
            }
            else
            {
                _logger?.LogInformation("数据库无需迁移.");
            }
        }
        /// <summary>
        /// 获得未迁移的迁移类
        /// </summary>
        /// <returns></returns>
        private List<Migration> GetPendingMigrations()
        {
            List<string> existingData = _maigrateRepository.GetExistingData(_dbOption);
            List<Migration> result = new();
            foreach (Type? migrationType in typeof(TDBOption).Assembly.GetTypes().Where(m => m.IsAssignableTo<Migration>() && !m.IsAbstract))
            {
                object migrationObj = Activator.CreateInstance(migrationType);
                if(migrationObj is Migration migration)
                {
                    if (!existingData.Contains(migration.MigrationID))
                    {
                        result.Add(migration);
                    }
                }
            }
            return result.OrderBy(m=>m.Index).ToList();
        }
    }
}
