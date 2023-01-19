using Materal.CacheHelper;
using Materal.Gateway.Common;
using Materal.Gateway.WebAPI.Filters;
using Materal.Gateway.WebAPI.Policies;
using Materal.Gateway.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Config;
using NLog.Web;
using Ocelot.Administration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Materal.Gateway.WebAPI
{
    public class Startup
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            ApplicationConfig.Init(configuration);
        }
        /// <summary>
        /// ���÷�������
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureOcelotServices(services);
            ConfigureMVCServices(services);
            ConfigureSwaggerServices(services);
            ConfigureNLogServices();
            ConfigureCorsServices(services);
            ConfigureCacheManagerServices(services);
            ConfigureOperationServices(services);
        }
        /// <summary>
        /// ����Http�ܵ�ģ��
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            ConfigureCors(app);
            ConfigureSwagger(app);
            app.UseHttpsRedirection();
            ConfigureStaticFiles(app);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string indexPath = Path.Combine(basePath, "wwwroot", "Index.html");
                    string webContent = "Materal.Gateway";
                    if (File.Exists(indexPath))
                    {
                        webContent = await File.ReadAllTextAsync(indexPath);
                    }
                    await context.Response.WriteAsync(webContent);
                });
                endpoints.MapControllers();
            });
            ConfigureOcelot(app);
        }
        #region ˽�з���
        #region ҵ��
        /// <summary>
        /// ����ҵ�����
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureOperationServices(IServiceCollection services)
        {
            services.AddSingleton<IRouteServices, RouteServices>();
        }
        #endregion
        #region �ڴ滺��
        /// <summary>
        /// ���û������
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureCacheManagerServices(IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
        #endregion
        #region MVC
        /// <summary>
        /// ����MVC����
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureMVCServices(IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add<GatewayAuthorizationFilter>();
                mvcOptions.Filters.Add<ExceptionFilter>();
                mvcOptions.SuppressAsyncSuffixInActionNames = true;
            });
            mvcBuilder.AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All);
                jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = new FirstUpperNamingPolicy();
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }
        #endregion
        #region Swagger
        /// <summary>
        /// ����Swagger����
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Materal.Gateway.WebAPI", 
                    Version = "v1",
                    Description = "�ṩWebAPI�ӿ�",
                    Contact = new OpenApiContact { Name = "Materal", Email = "cloomcmx1554@hotmail.com" }
                });
                OpenApiSecurityScheme bearerScheme = new OpenApiSecurityScheme
                {
                    Description = "������ͷ������JWT��Ȩ������:Authorization: {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                config.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, bearerScheme);
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {bearerScheme , new List<string>()}
                });
                //foreach (string path in _baseConfigure.SwaggerHelperXmlPathArray)
                //{
                //    config.IncludeXmlComments(path);
                //}
            });
        }
        /// <summary>
        /// ����Swagger
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Materal.Gateway.WebAPI v1"));
        }
        #endregion
        #region NLog
        /// <summary>
        /// ����NLog����
        /// </summary>
        private void ConfigureNLogServices()
        {
            string nLogConfigPatch = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NLog.config");
            NLogBuilder.ConfigureNLog(nLogConfigPatch).GetCurrentClassLogger();
            InstallationContext nLogInstallationContext = new InstallationContext();
            LogManager.Configuration.Install(nLogInstallationContext);
            LogManager.Configuration.Variables["MaxLogFileSaveDays"] = ApplicationConfig.NLogConfig.MaxLogFileSaveDays;
        }
        #endregion
        #region Ocelot
        /// <summary>
        /// ����Ocelot����
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureOcelotServices(IServiceCollection services)
        {
            services.AddOcelot()
                .AddConsul()
                .AddAdministration(ApplicationConfig.OcelotConfig.AdministrationUrl, ApplicationConfig.OcelotConfig.ClientSecret);
        }
        /// <summary>
        /// ����Ocelot
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureOcelot(IApplicationBuilder app)
        {
            app.UseOcelot().Wait();
        }
        #endregion
        #region Cors
        /// <summary>
        /// ����Cors����
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureCorsServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        }
        /// <summary>
        /// ����Cors
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureCors(IApplicationBuilder app)
        {
            app.UseCors("AllowAll");
        }
        #endregion
        #region ��̬�ļ�
        /// <summary>
        /// ����Cors
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureStaticFiles(IApplicationBuilder app)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(basePath, "wwwroot");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".json"] = "application/json";
            provider.Mappings[".woff"] = "application/font-woff";
            provider.Mappings[".woff2"] = "application/font-woff";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                    context.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                },
                RequestPath = "",
                ContentTypeProvider = provider,
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream"
            });
        }
        #endregion
        #endregion
    }
}
