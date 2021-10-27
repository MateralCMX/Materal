using System;
using Microsoft.EntityFrameworkCore;

namespace Materal.APP.Core
{
    public class DBContextHelper<T> where T : DbContext
    {
        private readonly T _dbContext;

        public DBContextHelper(T dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            ConsoleHelperBase.WriteLine("DBContextHelper", "正在初始化数据库......", null, ConsoleColor.White);
            _dbContext.Database.Migrate();
            ConsoleHelperBase.WriteLine("DBContextHelper", "数据库初始化完毕", null, ConsoleColor.White);
        }
    }
}
