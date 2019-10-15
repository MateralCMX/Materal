using Materal.CacheHelper;
using Materal.ConDep.Authority;
using Materal.ConDep.Common;
using Materal.ConDep.Manager;
using Materal.ConDep.Services;
using Materal.WebSocket;
using Materal.WebSocket.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;
using NLog.Extensions.Logging;
using System.Reflection;

namespace Materal.ConDep
{
    /// <summary>
    /// 持续部署服务依赖注入扩展
    /// </summary>
    public static class ConDepServerDIExtension
    {
        public static void AddConDepServer(this IServiceCollection services)
        {
            services.AddSingleton<ICacheManager,MemoryCacheManager>();
            services.AddTransient<ILoggerFactory, LoggerFactory>();
            services.AddCommandBus(Assembly.Load("Materal.ConDep.CommandHandlers"));
            services.AddControllers(type => ApplicationData.GetService(type), Assembly.Load("Materal.ConDep.Controllers"));
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Materal.ConDep.Authority"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.AddTransient<WebSocketServerHandler>();
            services.AddSingleton<IConDepServer, ConDepServerImpl>();
            services.AddSingleton<IUploadPoolService, UploadPoolServiceImpl>();
            services.AddSingleton<IAppManager, AppManagerImpl>();
            services.AddTransient<IAuthorityService, AuthorityServiceImpl>();
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
