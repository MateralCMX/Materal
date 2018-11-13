using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.MySqlRepository
{
    public class MySqlEntityFrameworkUnitOfWorkImpl<T> : EntityFrameworkUnitOfWorkImpl<T> where T : DbContext
    {
        public MySqlEntityFrameworkUnitOfWorkImpl(T context) : base(context)
        {
        }
    }
}
