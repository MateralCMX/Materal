using Microsoft.EntityFrameworkCore;
using NLog;

namespace Materal.APP.Core
{
    public class DBContextHelper<T> where T : DbContext
    {
        private readonly T _dbContext;
        private readonly Logger _logger;

        public DBContextHelper(T dbContext)
        {
            _dbContext = dbContext;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Migrate()
        {
            _logger.Info("正在初始化数据库......");
            _dbContext.Database.Migrate();
            _logger.Info("数据库初始化完毕");
        }
    }
}
