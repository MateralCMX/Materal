using Materal.TTA.Common;

namespace Materal.TTA.RedisRepository
{
    /// <summary>
    /// Redis仓储
    /// </summary>
    public interface IRedisRepository<T,in TPrimaryKeyType> : IRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
    }
}
