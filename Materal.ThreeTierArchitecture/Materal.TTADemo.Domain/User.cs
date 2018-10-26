using Materal.TTA.Common;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Materal.TTADemo.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    [Entity]
    public class User
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [BsonId]
        public Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
