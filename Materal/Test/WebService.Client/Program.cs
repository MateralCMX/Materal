﻿using Materal.Extensions.DependencyInjection;
using Materal.Utils.Extensions;
using Materal.Utils.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WebService.Client
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class Program
    {
        private const string url = "http://localhost:55667/WebService1.asmx";
        private const string serviceNamespace = "http://WebService.Server/";
        /// <summary>
        /// 主函数
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            MateralServices.Services = new ServiceCollection();
            HttpMessageHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (request, ceti, chain, errors) => true
            };
            HttpClient httpClient = new(handler);
            MateralServices.Services.TryAddSingleton(httpClient);
            MateralServices.Services.AddMateralUtils();
            MateralServices.Services.AddSingleton<IWebServiceHelper, WebServiceHelper>();
            MateralServices.ServiceProvider = MateralServices.Services.BuildServiceProvider();
            IWebServiceHelper webServiceClient = MateralServices.ServiceProvider.GetRequiredService<IWebServiceHelper>();
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
