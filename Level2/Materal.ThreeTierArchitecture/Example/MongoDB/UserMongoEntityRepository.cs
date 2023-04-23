using Materal.TTA.MongoDBRepository;
using System;

namespace Example.MongoDB
{
    public class UserMongoEntityRepository : MongoDBRepositoryImpl<UserMongoEntity, Guid>
    {
        public UserMongoEntityRepository() : base("mongodb://127.0.0.1:27017", nameof(UserMongoEntity), nameof(UserMongoEntity))
        {
        }
    }
}
