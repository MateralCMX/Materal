using Materal.Utils.MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace Materal.Test.UtilsTests.MongoTests
{
    [BsonIgnoreExtraElements]
    public class User : Person, IMongoEntity<Guid>
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; } = 18;
    }
}