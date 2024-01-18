namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TID"></typeparam>
    public class MongoRepository<T, TID> : MongoRepository<T>, IMongoRepository<T, TID>
        where T : class, IMongoEntity<TID>, new()
        where TID : struct
    {
    }
}
