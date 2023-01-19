using Materal.TTA.MongoDBRepository;
using System;

namespace Example.MongoDB
{
    public class UserMongoEntity : MongoEntity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}
