using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.MySqlRepository
{
    public abstract class MySqlEFUnitOfWorkImpl<T> : EFUnitOfWorkImpl<T> where T : DbContext
    {
        protected MySqlEFUnitOfWorkImpl(T context) : base(context)
        {
        }
    }
}
