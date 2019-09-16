using Demo.Common;
using Demo.Domain;
using Demo.MongoDBRepository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                ID = Guid.NewGuid(),
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
            _homeWorkMongoRepository.InsertMany(homeWorks);
            {
                FilterDefinitionBuilder<BsonDocument> builderFilter = Builders<BsonDocument>.Filter;
                FilterDefinition<BsonDocument> filterDefinition = builderFilter.Where(m => true);
                IFindFluent<BsonDocument, BsonDocument> document = _homeWorkMongoRepository.Find(filterDefinition);
                List<BsonDocument> documentList = document.ToList();
                List<HomeWork> homeWorkInfos = GetList<HomeWork>(documentList, typeof(MathHomeWork), typeof(PhysicsHomWork), typeof(ChemistryHomWork),
                    typeof(BiologyHomWork));
                Console.WriteLine(homeWorkInfos.Count);
            }
            {
                FilterDefinitionBuilder<BsonDocument> builderFilter = Builders<BsonDocument>.Filter;
                FilterDefinition<BsonDocument> filterDefinition = builderFilter.And(
                    builderFilter.Eq(nameof(HomeWork.Type), HomeWorkType.Math), 
                    builderFilter.Eq(nameof(HomeWork.Name), "数学作业1"));
                IFindFluent<BsonDocument, BsonDocument> document = _homeWorkMongoRepository.Find(filterDefinition);
                List<BsonDocument> documentList = document.ToList();
                List<HomeWork> homeWorkInfos = GetList<HomeWork>(documentList, typeof(MathHomeWork), typeof(PhysicsHomWork), typeof(ChemistryHomWork),
                    typeof(BiologyHomWork));
                Console.WriteLine(homeWorkInfos.Count);
            }
            _homeWorkMongoRepository.DeleteMany(m => true);
        }

        private static List<T> GetList<T>(IEnumerable<BsonDocument> elements, params Type[] childTypes)
            where T : class, new()
        {
            var result = new List<T>();
            if (childTypes == null) childTypes = new Type[0];
            Type tType = typeof(T);
            foreach (BsonDocument element in elements)
            {
                string targetTypeName = element.GetValue("_t").AsString;
                Type targetType = childTypes.FirstOrDefault(m => m.Name == targetTypeName);
                if (targetType == null && tType.Name == targetTypeName) targetType = tType;
                if (targetType == null) continue;
                ConstructorInfo constructorInfo = targetType.GetConstructor(new Type[0]);
                if (constructorInfo == null) continue;
                var data = (T)constructorInfo.Invoke(null);
                PropertyInfo[] propertyInfos = targetType.GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.Name == "ID")
                    {
                        BsonValue bsonValue = element.GetValue("_id");
                        propertyInfo.SetValue(data, bsonValue.AsGuid);
                    }
                    if (!element.TryGetValue(propertyInfo.Name, out BsonValue value)) continue;
                    BindPropertyValue(propertyInfo, value, data);
                }
                result.Add(data);
            }
            return result;
        }
        private static void BindPropertyValue<T>(PropertyInfo propertyInfo, BsonValue value, T data)
            where T : class, new()
        {
            switch (value.BsonType)
            {
                case BsonType.String:
                    propertyInfo.SetValue(data, value.AsString);
                    break;
                case BsonType.Binary:
                    if (propertyInfo.PropertyType == typeof(Guid))
                    {
                        propertyInfo.SetValue(data, value.AsGuid);
                    }
                    break;
                case BsonType.Boolean:
                    propertyInfo.SetValue(data, value.AsBoolean);
                    break;
                case BsonType.DateTime:
                    propertyInfo.SetValue(data, value.ToUniversalTime());
                    break;
                case BsonType.Null:
                    propertyInfo.SetValue(data, null);
                    break;
                case BsonType.Int32:
                    propertyInfo.SetValue(data,
                        propertyInfo.PropertyType.IsEnum
                            ? Enum.ToObject(propertyInfo.PropertyType, value.AsInt32)
                            : value.AsInt32);
                    break;
            }
        }
    }
}
