using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.Common
{
    public interface IEntityFrameworkUnitOfWork<in T> : IUnitOfWork where T: DbContext
    {
    }
}
