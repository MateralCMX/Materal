using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace MMB.Core.Reposiroty
{
    /// <summary>
    /// MMB仓储
    /// </summary>
    public partial interface ICacheMMBRepository<T, TPrimaryKeyType> : IMMBRepository<T, TPrimaryKeyType>, ICacheEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
    }
}
