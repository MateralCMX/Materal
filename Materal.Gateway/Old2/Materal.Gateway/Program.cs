using System.Threading.Tasks;

namespace Materal.Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ApplicationData.Init(args);
            while (!ApplicationData.IsShutDown)
            {
                await ApplicationData.StartAsync();
            }
        }
    }
}
