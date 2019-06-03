using BaseWebAPI;
using BaseWebAPI.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Common;

namespace Authority.WebAPI
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public const string AppName = "AuthorityWebAPI";
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 配置对象
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorityWebAPIServices();
            string basePath = PlatformServices.Default.Application.ApplicationBasePath;
            string[] swaggerHelperXmlPathArray =
            {
                Path.Combine(basePath, "Model.xml"),
                Path.Combine(basePath, "Authority.WebAPI.xml"),
                Path.Combine(basePath, "Authority.DataTransmitModel.xml"),
                Path.Combine(basePath, "Authority.PresentationModel.xml")
            };
            WebAPIHelper.BaseConfigureServices(services, new BaseConfigureServiceModel
            {
                AppName = AppName,
                SwaggerHelperXmlPathArray = swaggerHelperXmlPathArray
            });
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            WebAPIHelper.BaseConfigure(app, env, loggerFactory, AppName);
        }
    }
}
