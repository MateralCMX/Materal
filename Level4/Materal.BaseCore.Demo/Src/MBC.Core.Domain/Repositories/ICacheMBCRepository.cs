using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace MBC.Core.Domain.Repositories
{
    /// <summary>
    /// MBC仓储
    /// </summary>
    public partial interface ICacheMBCRepository<T, TPrimaryKeyType> : IMBCRepository<T, TPrimaryKeyType>, ICacheEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
    }
}
