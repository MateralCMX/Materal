using Materal.Abstractions;
using Materal.BaseCore.HttpClient;
using Materal.BaseCore.HttpClient.Extensions;
using Materal.Utils.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;
using RC.Authority.PresentationModel.User;
using System.Reflection;

namespace RC.ServerCenter.Test
{
    [TestClass]
    public class AuthorityHttpClientTest : BaseTest
    {
        private readonly UserHttpClient _userHttpClient;
        public AuthorityHttpClientTest() : base()
        {
            _userHttpClient = MateralServices.GetService<UserHttpClient>();
        }
        public override void AddServices(IServiceCollection services)
        {
            base.AddServices(services);
            services.AddHttpClientService("RC.ServerCenter", Assembly.Load("RC.Authority.HttpClient"));
        }
        [TestMethod]
        public async Task LoginTest()
        {
            try
            {
                LoginResultDTO? result = await _userHttpClient.LoginAsync(new LoginRequestModel
                {
                    Account = "Admin",
                    Password = "123456"
                });
                if (result == null) Assert.Fail("∑√Œ  ß∞‹"); 
            }
            catch (MateralHttpException ex)
            {
                string message = ex.GetExceptionMessage();
                Assert.Fail(message);
            }
        }
    }
}