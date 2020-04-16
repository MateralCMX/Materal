using System.Threading.Tasks;

namespace Example
{
    public class Program
    {
        public static async Task Main()
        {
            IExample example = new MongoDBExample();
            await example.StartAsync();
        }
    }
}
