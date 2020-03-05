using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerRepository
{
    public abstract class SqlServerEFUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T> where T : DbContext
    {
        protected SqlServerEFUnitOfWorkImpl(T context) : base(context)
        {
        }
    }
}
