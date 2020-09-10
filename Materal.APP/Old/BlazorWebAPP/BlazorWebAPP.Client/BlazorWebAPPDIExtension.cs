using System;
using System.Net.Http;
using System.Reflection;
using Authority.GrpcModel;
using BlazorWebAPP.Common;
using Grpc.Core;
using Grpc.Net.Client.Web;
using Materal.APP.Common;
using Materal.APP.GrpcModel;
using Materal.Common;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebAPP.Client
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class BlazorWebAPPDIExtension
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddBlazorWebAPPServices(this IServiceCollection services)
        {
            MateralConfig.PageStartNumber = 1;
            services.AddSingleton<IAuthorityManage, AuthorityManage>();
            AddGrpcClient<StringHelper.StringHelperClient>(services, BlazorWebAPPConfig.MateralAppUrlUrl);
            AddGrpcClient<User.UserClient>(services, BlazorWebAPPConfig.AuthorityUrl);
        }
        /// <summary>
        /// 添加一个Grpc客户端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="baseUrl"></param>
        public static void AddGrpcClient<T>(this IServiceCollection services, string baseUrl) where T : class
        {
            services.AddGrpcClient<T>(options =>
                {
                    options.Address = new Uri(baseUrl);
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var httpClientHandler = new HttpClientHandler
                    {
                        //ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                    var grpcWebHandler = new GrpcWebHandler(httpClientHandler);
                    return grpcWebHandler;
                })
                .ConfigureChannel(channelOptions =>
                {
                    channelOptions.Credentials = GetCallCredentials();
                });
        }
        /// <summary>
        /// 获得调用凭证
        /// </summary>
        /// <returns></returns>
        private static ChannelCredentials GetCallCredentials()
        {

            CallCredentials credentials = CallCredentials.FromInterceptor(async (context, metadata) =>
            {
                var authorityManage = ApplicationService.GetService<IAuthorityManage>();
                if (authorityManage != null && await authorityManage.IsLoginAsync())
                {
                    string token = await authorityManage.GetTokenAsync();
                    if (!string.IsNullOrEmpty(token))
                    {
                        metadata.Add("Authorization", $"Bearer {token}");
                    }
                }
            });
            return ChannelCredentials.Create(new SslCredentials(), credentials);
        }
    }
}
