using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.MySqlRepository
{
    public class MySqlEFUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T> where T : DbContext
    {
        public MySqlEFUnitOfWorkImpl(T context) : base(context)
        {
        }
    }
}
