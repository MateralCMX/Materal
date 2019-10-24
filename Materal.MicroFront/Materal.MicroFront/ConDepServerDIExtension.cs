using System.Reflection;
using Materal.CacheHelper;
using Materal.MicroFront.Common;
using Materal.Services;
using Materal.WebSocket;
using Materal.WebSocket.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using NLog.Extensions.Logging;

namespace Materal.MicroFront
{
    /// <summary>
    /// 持续部署服务依赖注入扩展
    /// </summary>
    public static class ConDepServerDIExtension
    {
        public static void AddConDepServer(this IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddTransient<ILoggerFactory, LoggerFactory>();
            services.AddCommandBus(Assembly.Load("Materal.MicroFront.CommandHandlers"));
            services.AddControllers(ApplicationData.GetService, Assembly.Load("Materal.MicroFront.Controllers"));
            services.AddTransient<WebSocketServerHandler>();
            services.AddSingleton<IMicroFrontServer, MicroFrontServerImpl>();
            services.AddTransient<IAuthorityService, AuthorityServiceImpl>();
            services.AddSingleton<IUploadPoolService, UploadPoolServiceImpl>();
            services.AddSingleton<IWebFileService, WebFileServiceImpl>();
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
            });
        }
    }
}
