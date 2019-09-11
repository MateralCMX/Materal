using Demo.Domain;
using Materal.TTA.MongoDBRepository;
using System;

namespace Demo.MongoDBRepository
{
    public interface IHomeWorkMongoRepository : IMongoDBRepository<HomeWork, Guid>
    {
    }
}
