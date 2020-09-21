using Materal.APP.Core;
using Materal.APP.HttpClient;
using Materal.Common;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using Tewr.Blazor.FileReader;
using WebAPP.MateralUI;

namespace WebAPP
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class WebAPPDIExtension
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddWebAPPServices(this IServiceCollection services)
        {
            MateralConfig.PageStartNumber = 1;
            services.AddMateralUI();
            services.AddFileReaderService(options =>
            {
                options.UseWasmSharedBuffer = true;
            });
            services.AddSingleton<IAuthorityManage, JsAuthorityManageImpl>();
            services.AddTransient<MessageManage>();
            services.AddTransient<ExceptionManage>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("WebAPP.HttpClientImpl"))
                .Where(c => c.Name.EndsWith("HttpClientImpl"))
                .AsPublicImplementedInterfaces();
            services.AddAutoMapperService(Assembly.Load("WebAPP"));
        }
    }
}
