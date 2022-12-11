using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Materal.TTA.EFRepository
{
    public class MigrateHelper<T>
        where T : DbContext
    {
        private readonly T dbContext;
        private readonly ILogger<MigrateHelper<T>>? _logger;
        public MigrateHelper(T dbContext, ILogger<MigrateHelper<T>>? logger = null)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        public async Task MigrateAsync()
        {
            IEnumerable<string> migrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (migrations.Any())
            {
                _logger?.LogInformation("正在迁移数据库...");
                await dbContext.Database.MigrateAsync();
                _logger?.LogInformation("数据库迁移完毕");
            }
            else
            {
                _logger?.LogInformation("数据无需迁移.");
            }
        }
    }
}
