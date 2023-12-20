using Materal.Abstractions;
using Materal.Utils;
using Materal.Utils.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WebService.Client
{
    public class Program
    {
        private const string url = "http://localhost:55667/WebService1.asmx";
        private const string serviceNamespace = "http://WebService.Server/";
        public static async Task Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            HttpMessageHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (request, ceti, chain, errors) => true
            };
            HttpClient httpClient = new(handler);
            serviceCollection.TryAddSingleton(httpClient);
            serviceCollection.AddMateralUtils();
            serviceCollection.AddSingleton<IWebServiceHelper, WebServiceHelper>();
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            IServiceProvider Services = MateralServices.Services;
            IWebServiceHelper webServiceClient = Services.GetRequiredService<IWebServiceHelper>();
            string serviceName = "GetUserByUserInfo";
            UserInfo result = new() { Name = "Materal", Age = 18 };
            UserInfo? soap1_1Result = await webServiceClient.SendSoapAsync<UserInfo>(url, serviceName, serviceNamespace, new() { ["user"] = result }, SoapVersionEnum.Soap1_1);
            UserInfo? soap1_2Result = await webServiceClient.SendSoapAsync<UserInfo>(url, serviceName, serviceNamespace, new() { ["user"] = result }, SoapVersionEnum.Soap1_2);
        }
    }
    public class UserInfo
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int? TestNull { get; set; }
        public ScoreInfo ScoreValue { get; set; } = new() { Name = "C#", Score = 100 };
        public int[] ArrayValues { get; set; } = [1, 2, 3];
        public List<ScoreInfo> Scores { get; set; } =
        [
            new() { Name = "语文", Score = 100 },
            new() { Name = "数学", Score = 99 }
        ];
    }
    public class ScoreInfo
    {
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public int? TestNull { get; set; }
    }
}
