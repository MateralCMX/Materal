using Materal.MergeBlock.Web.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Materal.MergeBlock.Web
{
    /// <summary>
    /// Swagger模块
    /// </summary>
    public class WebModule() : MergeBlockModule("Web模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            IConfigurationSection? section = context.Configuration?.GetSection(WebOptions.ConfigKey);
            WebOptions? webOptions = null;
            if (section is not null)
            {
                context.Services.Configure<WebOptions>(section);
                webOptions = section.GetConfigItem<WebOptions>();
            }
            IMvcBuilder mvcBuilder = context.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = true;
                options.Filters.Add<ActionPageQueryFilterAttribute>();
            }).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
            .AddNewtonsoftJson(options =>
            {
                if (webOptions is not null && webOptions.UsePascalCase)
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                }
            });
            context.Services.AddSingleton(mvcBuilder);
            MergeBlockContext? mergeBlockContext = context.Services.GetSingletonInstance<MergeBlockContext>();
            if (mergeBlockContext is not null)
            {
                foreach (Assembly assembly in mergeBlockContext.MergeBlockAssemblies)
                {
                    mvcBuilder.AddApplicationPart(assembly);
                }
            }
            context.Services.AddEndpointsApiExplorer();//添加API探索器
            if (webOptions is not null)
            {
                if (webOptions.HttpBodyMaxSize is not null)
                {
                    context.Services.Configure<KestrelServerOptions>(options =>
                    {
                        options.Limits.MaxRequestBodySize = webOptions.HttpBodyMaxSize.Value;
                    });
                }
            }
        }
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            AdvancedContext advancedContext = context.ServiceProvider.GetRequiredService<AdvancedContext>();
            if (advancedContext.App is not WebApplication webApplication) return;
            webApplication.Use(async (httpContext, next) => // 使请求可重复读取
            {
                httpContext.Request.EnableBuffering();
                await next.Invoke(httpContext);
            });
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            AdvancedContext advancedContext = context.ServiceProvider.GetRequiredService<AdvancedContext>();
            if (advancedContext.App is not WebApplication webApplication) return;
            webApplication.MapControllers();
            AutoUseHttpsRedirection(webApplication);
        }
        /// <summary>
        /// 自动使用Https重定向
        /// </summary>
        /// <param name="app"></param>
        private static void AutoUseHttpsRedirection(WebApplication app)
        {
            List<Uri> urlsList = []; //URLS
            IConfiguration configuration = app.Services.GetRequiredService<IConfiguration>();
            string? urlsValue = configuration.GetConfigItemToString("URLS");
            if (string.IsNullOrWhiteSpace(urlsValue) && app.Environment.IsDevelopment())
            {
                urlsValue ??= configuration.GetConfigItemToString("ASPNETCORE_URLS");
            }
            urlsValue ??= "http://localhost:5000";
            string[] urls = urlsValue.Split(";");
            foreach (string url in urls)
            {
                urlsList.Add(new(url));
            }
            #region 处理Url相关
            if (urlsList.Any(m => m.Scheme == "https"))
            {
                app.UseHttpsRedirection(); //启用HTTPS重定向
            }
            #endregion
        }
    }
}
