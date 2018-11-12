using Materal.TTA.Common;

namespace Materal.TTA.MongoDBRepository
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public interface IMongoDBRepository<T,in TPrimaryKeyType> : IRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
    }
}
