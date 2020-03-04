using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.EFRepository
{
    public interface IEFUnitOfWork<in T> : IUnitOfWork where T: DbContext
    {
    }
}
