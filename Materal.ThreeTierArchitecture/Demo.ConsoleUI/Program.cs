using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Common;
using Demo.Domain;
using Demo.MongoDBRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Demo.ConsoleUI
{
    public class Program
    {
        private static IHomeWorkMongoRepository _homeWorkMongoRepository;
        public static void Main(string[] args)
        {
            _homeWorkMongoRepository = new HomeWorkMongoRepositoryImpl();
            var classInfo = new Class
            {
                ID = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                Name = "233班",
                UpdateTime = DateTime.MinValue,
                Remark = "初中一年级"
            };
            var student = new Student
            {
                ID = Guid.Parse("c8150f42-04d8-4e1a-b40b-9f5a44bf695c"),
                Age = 24,
                BelongClassID = classInfo.ID,
                CreateTime = DateTime.Now,
                Name = "Materal",
                Sex = SexEnum.Male,
                UpdateTime = DateTime.MinValue
            };
            var homeWorks = new List<HomeWork>
            {
                new MathHomeWork
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.MinValue,
                    Type = HomeWorkType.Math,
                    Name = "数学作业1",
                    Computation = 1
                },
                new PhysicsHomWork
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.MinValue,
                    Type = HomeWorkType.Physics,
                    Name = "物理作业1",
                    Formula = "e=mc^2"
                },
                new ChemistryHomWork
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.MinValue,
                    Type = HomeWorkType.Chemistry,
                    Name = "化学作业1",
                    Element = "H"
                },
                new BiologyHomWork
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.MinValue,
                    Type = HomeWorkType.Biology,
                    Name = "生物作业1",
                    CellName = "植物细胞"
                },
                new MathHomeWork
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.MinValue,
                    Type = HomeWorkType.Math,
                    Name = "数学作业2",
                    Computation = 99
                },
                new PhysicsHomWork
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.MinValue,
                    Type = HomeWorkType.Physics,
                    Name = "物理作业2",
                    Formula = "F=ma"
                },
                new ChemistryHomWork
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.MinValue,
                    Type = HomeWorkType.Chemistry,
                    Name = "化学作业2",
                    Element = "Si"
                },
                new BiologyHomWork
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.MinValue,
                    Type = HomeWorkType.Biology,
                    Name = "生物作业2",
                    CellName = "动物细胞"
                }
            };
            Run(classInfo, student, homeWorks);
        }

        public static void Run(Class @class, Student student, List<HomeWork> homeWorks)
        {
            _homeWorkMongoRepository.SelectCollection(student.ID.ToString());
            //_homeWorkMongoRepository.InsertMany(homeWorks);
            FilterDefinitionBuilder<BsonDocument> builderFilter = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filterDefinition = builderFilter.Where(m => true);
            List<BsonDocument> document = _homeWorkMongoRepository.Find(filterDefinition);
            IFindFluent<HomeWork, HomeWork> findDocument = _homeWorkMongoRepository.FindDocument(m => m.Type == HomeWorkType.Math);
            List<MathHomeWork> homeWorkInfos = findDocument.As<MathHomeWork>().ToList();
            foreach (BsonDocument bsonDocument in document)
            {
            }
        }

        public static async Task RunAsync(Class @class, Student student, List<HomeWork> homeWorks)
        {

        }
    }
}
