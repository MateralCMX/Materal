using Materal.TTA.Common;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF读写分离缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface ICacheEFSubordinateRepository<T, TPrimaryKeyType> : ICacheSubordinateRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
