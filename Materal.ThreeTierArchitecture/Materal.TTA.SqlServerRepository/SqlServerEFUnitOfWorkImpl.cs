using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerRepository
{
    public class SqlServerEFUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T> where T : DbContext
    {
        public SqlServerEFUnitOfWorkImpl(T context) : base(context)
        {
        }
    }
}
