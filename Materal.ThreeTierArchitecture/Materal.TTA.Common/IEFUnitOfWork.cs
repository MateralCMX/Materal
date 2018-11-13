using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.Common
{
    public interface IEFUnitOfWork<in T> : IUnitOfWork where T: DbContext
    {
    }
}
