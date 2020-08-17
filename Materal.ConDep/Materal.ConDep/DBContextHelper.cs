using Microsoft.EntityFrameworkCore;

namespace Materal.ConDep
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
            _dbContext.Database.Migrate();
        }
    }
}
