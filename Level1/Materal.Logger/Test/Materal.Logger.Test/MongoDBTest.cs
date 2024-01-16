using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Logger.Test
{
    [TestClass]
    public class MongoDBTest
    {
        [TestMethod]
        public async Task MongoDBGetCollectionTest()
        {
            MongoClient client = new("mongodb://localhost:27017/");
            string dbName = "MateralLogger";
            string collectionName = "Logs";
            IMongoDatabase db = client.GetDatabase(dbName);
            IAsyncCursor<string> collectionNames = await db.ListCollectionNamesAsync(new ListCollectionNamesOptions
            {
                Filter = new BsonDocument("name", collectionName)
            });
            if(!collectionNames.Any())
            {
                await db.CreateCollectionAsync(collectionName, new CreateCollectionOptions
                {
                    TimeSeriesOptions = new TimeSeriesOptions("CreateTime")
                });
            }
        }
    }
}