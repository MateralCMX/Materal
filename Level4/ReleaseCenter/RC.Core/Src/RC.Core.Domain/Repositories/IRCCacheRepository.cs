using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace RC.Core.Domain.Repositories
{
    /// <summary>
    /// RC仓储
    /// </summary>
    public partial interface IRCCacheRepository<T, TPrimaryKeyType> : IRCRepository<T, TPrimaryKeyType>, ICacheEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
    }
}
