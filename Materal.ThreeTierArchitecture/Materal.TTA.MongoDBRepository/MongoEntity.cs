using Materal.TTA.Common;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Materal.TTA.MongoDBRepository
{
    /// <inheritdoc />
    /// <summary>
    /// Mongo实体
    /// </summary>
    [Serializable]
    [BsonIgnoreExtraElements]
    public abstract class MongoEntity<TIdentifier> : IEntity<TIdentifier>
    {
        /// <inheritdoc />
        /// <summary>
        /// ID全局唯一标识
        /// </summary>
        [BsonId]
        public TIdentifier ID { get; set; }
    }
}
