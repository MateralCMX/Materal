using System;
using Demo.Domain;
using Materal.TTA.MongoDBRepository;

namespace Demo.MongoDBRepository
{
    public class HomeWorkMongoRepositoryImpl : MongoDBRepositoryImpl<HomeWork, Guid>, IHomeWorkMongoRepository
    {
        public HomeWorkMongoRepositoryImpl() : base("mongodb://127.0.0.1:27017", "StudentJob")
        {
        }
    }
}
