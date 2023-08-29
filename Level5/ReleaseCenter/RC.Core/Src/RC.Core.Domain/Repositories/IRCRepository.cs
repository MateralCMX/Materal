using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace RC.Core.Domain.Repositories
{
    /// <summary>
    /// RC仓储
    /// </summary>
    public partial interface IRCRepository<T, TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
    }
}
