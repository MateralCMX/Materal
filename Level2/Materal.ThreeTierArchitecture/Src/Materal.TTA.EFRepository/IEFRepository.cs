using Materal.TTA.Common;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface IEFRepository<T, in TPrimaryKeyType> : IRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
