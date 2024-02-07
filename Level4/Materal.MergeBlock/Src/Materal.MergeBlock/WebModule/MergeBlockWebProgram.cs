using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// MergeBlockWeb程序
    /// </summary>
    public class MergeBlockWebProgram : MergeBlockProgram<MergeBlockWebModule, WebModuleInfo, WebConfigServiceContext, WebApplicationContext>
    {
        private WebApplicationBuilder? _builder;
        private WebApplication? _app;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <param name="autoRemoveAssemblies"></param>
        /// <returns></returns>
        public override async Task RunAsync(string[] args, bool autoRemoveAssemblies = true)
        {
            _builder = WebApplication.CreateBuilder(args);
            _builder.Host.UseServiceProviderFactory(new WebModuleMateralServiceProviderFactory());//替换服务提供者工厂
            await ConfigModuleAsync(_builder.Services, _builder.Configuration, autoRemoveAssemblies);
            _app = _builder.Build();
            WebApplicationContext context = await InitModuleAsync(_app.Services);
            await _app.RunAsync();
            await CloseModuleAsync(context);
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected override WebConfigServiceContext GetConfigServiceContext()
        {
            if (_builder is null) throw new MergeBlockException("未初始化WebApplicationBuilder");
            return new(_builder);
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected override WebApplicationContext GetApplicationContext()
        {
            if (_app is null) throw new MergeBlockException("未初始化WebApplication");
            return new(_app.Services, _app);
        }
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async Task ConfigServiceBeforeAsync(WebConfigServiceContext context)
        {
            context.Services.AddHostedService<MergeBlockHostedService>();
            #region 跨域
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });
            #endregion
            List<Assembly> assemblies = [];
            await RunModuleAsync(m =>
            {
                if(assemblies.Any(n => n == m.ModuleType.Assembly)) return;
                MergeBlockAssemblyAttribute? mergeBlockAssemblyAttribute = m.ModuleType.Assembly.GetCustomAttribute<MergeBlockAssemblyAttribute>();
                if (mergeBlockAssemblyAttribute is null || !mergeBlockAssemblyAttribute.HasController) return;
                context.MvcBuilder.AddApplicationPart(m.ModuleType.Assembly);
                assemblies.Add(m.ModuleType.Assembly);
            }, false);//加载控制器与视图
            await base.ConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async Task ConfigServiceAfterAsync(WebConfigServiceContext context)
        {
            context.Services.AddResponseCompression();//添加响应压缩
            context.Services.AddEndpointsApiExplorer();//添加API探索器
            await base.ConfigServiceAfterAsync(context);
        }
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override Task ApplicationInitBeforeAsync(WebApplicationContext context)
        {
            context.WebApplication.Use(async (context, next) => // 使请求可重复读取
            {
                context.Request.EnableBuffering();
                await next.Invoke(context);
            });
            context.WebApplication.UseCors();//跨域
            return base.ApplicationInitBeforeAsync(context);
        }
        /// <summary>
        /// 应用程序初始化后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async Task ApplicationInitAfterAsync(WebApplicationContext context)
        {
            context.WebApplication.MapControllers();
            List<Uri> urlsList = []; //URLS
            IConfiguration configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            string? urlsValue = configuration.GetConfigItemToString("URLS");
            if (context.WebApplication.Environment.IsDevelopment() && string.IsNullOrWhiteSpace(urlsValue))
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
                context.WebApplication.UseHttpsRedirection(); //启用HTTPS重定向
            }
            #endregion
            await base.ApplicationInitAfterAsync(context);
        }
    }
}
