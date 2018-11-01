using Materal.WindowsHelper;
using System;
using System.Threading.Tasks;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        static void Main()
        {
            Guid id = Guid.NewGuid();
            Guid id2 = Guid.Parse(id.ToString());
            Console.WriteLine(id.Equals(id2));
            //Task.Run(async () => { await Init(); });
            Console.ReadKey();
        }

        public static async Task Init()
        {
            string output = await CmdHelper.RunCmdCommandsAsync("ipconfig");
            Console.WriteLine(output);
            //IMongoDatabase mongoDatabase = new MongoClient("mongodb://220.165.9.44:13398").GetDatabase("BeiDouDB");
            //IMongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>("DeviceLocation");
            //FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            //var minDate = new DateTime(2018, 10, 1, 0, 0, 0);
            //var maxDate = new DateTime(2018, 10, 26, 23, 59, 59);
            //FilterDefinition<BsonDocument> filter = builder.Eq("DeviceID", Guid.Parse("808cbe9f-e08e-4f1d-82b4-07745089d484")) & builder.Gte("CreateTime", minDate) & builder.Lte("CreateTime", maxDate);
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            //List<BsonDocument> result = await collection.Find(filter).ToListAsync();
            //stopwatch.Stop();
            //Console.WriteLine($"{result.Count}条数据,耗时:{stopwatch.ElapsedTicks}");
        }
    }
}
