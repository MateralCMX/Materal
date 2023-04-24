using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Materal.TTA.EFRepository
{
    public class MigrateHelper<T>: IDisposable
        where T : DbContext
    {
        private readonly T _dbContext;
        private readonly ILogger<MigrateHelper<T>>? _logger;
        public MigrateHelper(T dbContext, ILogger<MigrateHelper<T>>? logger = null)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task MigrateAsync()
        {
            IEnumerable<string> migrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (migrations.Any())
            {
                _logger?.LogInformation("正在迁移数据库...");
                try
                {
                    await _dbContext.Database.MigrateAsync();
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

    }
}
