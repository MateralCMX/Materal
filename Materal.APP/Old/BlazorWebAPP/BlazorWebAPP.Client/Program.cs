using Materal.APP.GrpcModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BlazorWebAPP.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
            {
                return true;
            };
            IServiceCollection services = new ServiceCollection();
            services.AddBlazorWebAPPServices();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var stringHelperClient = serviceProvider.GetService<StringHelper.StringHelperClient>();
            StringHandlerGrpcResultModel resultModel = await stringHelperClient.DesDecryptionAsync(new StringHandlerGrpcRequestModel
            {
                Value = "qwer"
            });
        }
    }
}
