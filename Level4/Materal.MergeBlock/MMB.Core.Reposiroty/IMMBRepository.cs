using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace MMB.Core.Reposiroty
{
    /// <summary>
    /// MMB仓储
    /// </summary>
    public partial interface IMMBRepository<T, TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
    }
}
