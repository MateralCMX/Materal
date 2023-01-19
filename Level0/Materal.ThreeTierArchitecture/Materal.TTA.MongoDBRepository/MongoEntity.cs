using Materal.TTA.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace Materal.TTA.MongoDBRepository
{
    /// <inheritdoc />
    /// <summary>
    /// Mongo实体
    /// </summary>
    [Serializable]
    [BsonIgnoreExtraElements]
    public abstract class MongoEntity<TIdentifier> : IEntity<TIdentifier>
        where TIdentifier: struct
    {
        /// <inheritdoc />
        /// <summary>
        /// ID全局唯一标识
        /// </summary>
        [BsonId]
        public TIdentifier ID { get; set; }
    }
}
