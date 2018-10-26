using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Materal.StringHelper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => { await Init(); });
            Console.ReadKey();
        }

        public static async Task Init()
        {
            IMongoDatabase mongoDatabase = new MongoClient("mongodb://220.165.9.44:13398").GetDatabase("BeiDouDB");
            IMongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>("DeviceLocation");
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            var minDate = new DateTime(2018, 10, 1, 0, 0, 0);
            var maxDate = new DateTime(2018, 10, 26, 23, 59, 59);
            FilterDefinition<BsonDocument> filter = builder.Eq("DeviceID",Guid.Parse("808cbe9f-e08e-4f1d-82b4-07745089d484")) & builder.Gte("CreateTime", minDate) & builder.Lte("CreateTime", maxDate);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            List<BsonDocument> result = await collection.Find(filter).ToListAsync();
            stopwatch.Stop();
            Console.WriteLine($"{result.Count}条数据,耗时:{stopwatch.ElapsedTicks}");
        }
    }
}
