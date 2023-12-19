﻿using Materal.Utils.WebServiceClient;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Test.UtilsTests
{
    [TestClass]
    public class WebServiceTest : BaseTest
    {
        public override void AddServices(IServiceCollection services)
        {
            base.AddServices(services);
            services.AddSingleton<IWebServiceClient, WebServiceClient>();
        }
        private const string url = "http://localhost:55667/WebService1.asmx";
        private const string serviceNamespace = "http://WebService.Server/";
        [TestMethod]
        public async Task HelloWorldTestAsync()
        {
            IWebServiceClient webServiceClient = Services.GetRequiredService<IWebServiceClient>();
            string serviceName = "HelloWorld";
            const string result = "Hello World";
            string? soap1_1Result = await webServiceClient.SendSoapAsync<string>(url, serviceName, serviceNamespace, null, SoapVersionEnum.Soap1_1);
            Assert.AreEqual(result, soap1_1Result);
            string? soap1_2Result = await webServiceClient.SendSoapAsync<string>(url, serviceName, serviceNamespace, null, SoapVersionEnum.Soap1_2);
            Assert.AreEqual(result, soap1_2Result);
        }
        [TestMethod]
        public async Task GetUserTestAsync()
        {
            IWebServiceClient webServiceClient = Services.GetRequiredService<IWebServiceClient>();
            string serviceName = "GetUser";
            UserInfo result = new() { Name = "Materal", Age = 18 };
            UserInfo? soap1_1Result = await webServiceClient.SendSoapAsync<UserInfo>(url, serviceName, serviceNamespace, null, SoapVersionEnum.Soap1_1);
            Assert.AreEqual(result.Name, soap1_1Result?.Name);
            Assert.AreEqual(result.Age, soap1_1Result?.Age);
            UserInfo? soap1_2Result = await webServiceClient.SendSoapAsync<UserInfo>(url, serviceName, serviceNamespace, null, SoapVersionEnum.Soap1_2);
            Assert.AreEqual(result.Name, soap1_2Result?.Name);
            Assert.AreEqual(result.Age, soap1_2Result?.Age);
        }
        [TestMethod]
        public async Task GetUsersTestAsync()
        {
            IWebServiceClient webServiceClient = Services.GetRequiredService<IWebServiceClient>();
            string serviceName = "GetUsers";
            const int resultCount = 2;
            List<UserInfo>? soap1_1Result = await webServiceClient.SendSoapAsync<List<UserInfo>>(url, serviceName, serviceNamespace, null, SoapVersionEnum.Soap1_1);
            Assert.AreEqual(resultCount, soap1_1Result?.Count);
            UserInfo[]? soap1_2Result = await webServiceClient.SendSoapAsync<UserInfo[]>(url, serviceName, serviceNamespace, null, SoapVersionEnum.Soap1_2);
            Assert.AreEqual(resultCount, soap1_2Result?.Length);
        }
        [TestMethod]
        public async Task GetUserByUserInfoTestAsync()
        {
            IWebServiceClient webServiceClient = Services.GetRequiredService<IWebServiceClient>();
            string serviceName = "GetUserByUserInfo";
            UserInfo result = new() { Name = "Materal", Age = 18 };
            UserInfo? soap1_1Result = await webServiceClient.SendSoapAsync<UserInfo>(url, serviceName, serviceNamespace, new() { ["user"] = result }, SoapVersionEnum.Soap1_1);
            Assert.AreEqual(result.Name, soap1_1Result?.Name);
            Assert.AreEqual(result.Age, soap1_1Result?.Age);
            UserInfo? soap1_2Result = await webServiceClient.SendSoapAsync<UserInfo>(url, serviceName, serviceNamespace, new() { ["user"] = result }, SoapVersionEnum.Soap1_2);
            Assert.AreEqual(result.Name, soap1_2Result?.Name);
            Assert.AreEqual(result.Age, soap1_2Result?.Age);
        }
        [TestMethod]
        public async Task GetUserByNameAndAgeTestAsync()
        {
            IWebServiceClient webServiceClient = Services.GetRequiredService<IWebServiceClient>();
            string serviceName = "GetUserByNameAndAge";
            UserInfo result = new() { Name = "Materal", Age = 18 };
            UserInfo? soap1_1Result = await webServiceClient.SendSoapAsync<UserInfo>(url, serviceName, serviceNamespace, new() { ["name"] = result.Name, ["age"] = result.Age }, SoapVersionEnum.Soap1_1);
            Assert.AreEqual(result.Name, soap1_1Result?.Name);
            Assert.AreEqual(result.Age, soap1_1Result?.Age);
            UserInfo? soap1_2Result = await webServiceClient.SendSoapAsync<UserInfo>(url, serviceName, serviceNamespace, new() { ["name"] = result.Name, ["age"] = result.Age }, SoapVersionEnum.Soap1_2);
            Assert.AreEqual(result.Name, soap1_2Result?.Name);
            Assert.AreEqual(result.Age, soap1_2Result?.Age);
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
}