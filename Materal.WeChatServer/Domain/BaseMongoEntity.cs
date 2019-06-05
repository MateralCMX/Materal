using System;
using Materal.TTA.MongoDBRepository;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    public class BaseMongoEntity<T> : MongoEntity<T>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected BaseMongoEntity()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.MinValue;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }
    }
}
