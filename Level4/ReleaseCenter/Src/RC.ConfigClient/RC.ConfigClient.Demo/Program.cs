using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;

namespace RC.ConfigClient.Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<string> a = new()
            {
                "a","b"
            };
            if(a.Contains("a"))
            {

            }
            if (a.Contains("b"))
            {

            }
            if (a.Contains("c"))
            {

            }
            ConfigurationItemHttpClient configurationItemHttpClient = new("http://175.27.254.187:8700/RCES_DevAPI");
            ICollection<ConfigurationItemListDTO>? items = await configurationItemHttpClient.GetDataAsync(new QueryConfigurationItemRequestModel
            {
                PageIndex = 1,
                PageSize = int.MaxValue,
                ProjectName = "MateralReleaseCenter",
                NamespaceNames = new()
                {
                    "Application",
                    "ConfigClient"
                }
            });
            if (items == null) return;
            Console.WriteLine(items.Count);
        }
    }
}