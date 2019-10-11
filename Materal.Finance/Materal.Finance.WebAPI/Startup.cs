using Core;
using Core.Model;
using Materal.Common;
using Materal.Finance.WebAPI.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Config;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Materal.Finance.WebAPI
{
    public class Startup
    {
        private readonly BaseConfigureServiceModel _baseConfigure;
        /// <summary>
        /// Startup
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ApplicationData.AppName = "技术部任务系统";
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerHelperXmlPathArray =
            {
                Path.Combine(basePath, "DevOA.WebAPI.xml"),
                Path.Combine(basePath, "Authority.DataTransmitModel.xml"),
                Path.Combine(basePath, "Authority.PresentationModel.xml")
            };
            _baseConfigure = new BaseConfigureServiceModel
            {
                AppName = ApplicationData.AppName,
                SwaggerHelperXmlPathArray = swaggerHelperXmlPathArray
            };
        }
        /// <summary>
        /// 配置对象
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            ApplicationData.Services = services;
            services.AddWebAPIServices();
            services.AddResponseCompression();
            #region 帮助文档
            /*Swashbuckle.AspNetCore*/
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc(_baseConfigure.AppName, new Info
                {
                    Version = "0.1",
                    Title = _baseConfigure.AppName,
                    Description = "提供WebAPI接口",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Materal", Email = "cloomcmx1554@hotmail.com", Url = "" }
                });
                foreach (string path in _baseConfigure.SwaggerHelperXmlPathArray)
                {
                    m.IncludeXmlComments(path);
                }
            });
            #endregion
            #region 跨域
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));
            #endregion
            #region 配置MVC
            services.AddMvc(options => options.Filters.Add<ExceptionProcessFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            #endregion
            ApplicationData.Build();
            return ApplicationData.ServiceProvider;
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseResponseCompression();
            app.UseCors("AllowAll");
            #region 配置Materal
            MateralConfig.PageStartNumber = 1;
            #endregion
            #region 配置Nlog
            NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            LogManager.Configuration.Install(new InstallationContext());
            LogManager.Configuration.Variables["AppName"] = _baseConfigure.AppName;
            #endregion
            #region 配置帮助文档
            app.UseSwagger(c => { c.RouteTemplate = "{documentName}/swagger.json"; })
                .UseSwaggerUI(c => { c.SwaggerEndpoint($"/{_baseConfigure.AppName}/swagger.json", $"{_baseConfigure.AppName}"); });
            #endregion
            app.UseMvc();
        }
    }
}
