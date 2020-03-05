using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqliteRepository
{
    public abstract class SqliteEFUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T> where T : DbContext
    {
        protected SqliteEFUnitOfWorkImpl(T context) : base(context)
        {
        }
    }
}
