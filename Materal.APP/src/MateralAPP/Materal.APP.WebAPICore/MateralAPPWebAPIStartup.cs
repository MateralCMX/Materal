using Materal.APP.Core;
using Materal.APP.Core.ConfigModels;
using Materal.APP.TFMS.Core;
using Materal.APP.WebAPICore.Filters;
using Materal.APP.WebCore.Policies;
using Materal.CacheHelper;
using Materal.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Materal.APP.WebAPICore
{
    /// <summary>
    /// WebAPIStartup帮助类
    /// </summary>
    public abstract class MateralAPPWebAPIStartup
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        protected MateralAPPWebAPIStartup(IConfiguration configuration)
        {
            ApplicationConfig.Init(configuration);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="webAPIStartupConfig"></param>
        protected void Init(WebAPIStartupConfig webAPIStartupConfig)
        {
            ApplicationConfig.Init(webAPIStartupConfig);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ConfigureMVCServices(services);
            ConfigureResponseCompressionServices(services);
            ConfigureAuthenticationServices(services);
            if (ApplicationConfig.ShowException)
            {
                ConfigureSwaggerServices(services);
            }
            ConfigureCorsServices(services);
            ConfigureCacheManagerServices(services);
        }

        /// <summary>
        /// 配置Http管道模型
        /// </summary>
        /// <param name="app"></param>
        /// <param name="en"></param>
        /// <param name="lifetime"></param>
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment en, IHostApplicationLifetime lifetime)
        {
            if (en.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (ApplicationConfig.BaseUrlConfig.IsSSL)
            {
                app.UseHttpsRedirection();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            ConfigureCors(app);
            if (ApplicationConfig.ShowException)
            {
                ConfigureSwagger(app);
            }
            MateralConfig.PageStartNumber = 1;
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            NLogManage.Register();
        }
        #region 保护方法
        #region MVC
        /// <summary>
        /// 配置MVC服务
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureMVCServices(IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add(new AuthorizeFilter());
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
        #region 响应压缩
        /// <summary>
        /// 配置响应压缩服务
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureResponseCompressionServices(IServiceCollection services)
        {
            services.AddResponseCompression();
        }
        #endregion
        #region 鉴权
        /// <summary>
        /// 配置鉴权服务
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureAuthenticationServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(ApplicationConfig.JWTConfig.KeyBytes),
                        ValidateIssuer = true,
                        ValidIssuer = ApplicationConfig.JWTConfig.Issuer,
                        ValidateAudience = true,
                        ValidAudience = ApplicationConfig.JWTConfig.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(ApplicationConfig.JWTConfig.ExpiredTime)
                    };
                });
        }

        #endregion
        #region Swagger
        /// <summary>
        /// 配置Swagger服务
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{ApplicationConfig.WebAPIStartupConfig.AppName}.WebAPI",
                    Version = "v1",
                    Description = "提供WebAPI接口",
                    Contact = new OpenApiContact { Name = "Materal", Email = "cloomcmx1554@hotmail.com" }
                });
                OpenApiSecurityScheme bearerScheme = new OpenApiSecurityScheme
                {
                    Description = "在请求头部加入JWT授权。例子:Authorization:Bearer {token}",
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
                if (ApplicationConfig.WebAPIStartupConfig.SwaggerXmlPathArray == null || ApplicationConfig.WebAPIStartupConfig.SwaggerXmlPathArray.Count <= 0) return;
                foreach (string path in ApplicationConfig.WebAPIStartupConfig.SwaggerXmlPathArray)
                {
                    config.IncludeXmlComments(path);
                }
            });
        }
        /// <summary>
        /// 配置Swagger
        /// </summary>
        /// <param name="app"></param>
        protected virtual void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(m =>
            {
                m.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ApplicationConfig.WebAPIStartupConfig.AppName}.WebAPI v1"));
        }
        #endregion
        #region Cors
        /// <summary>
        /// 配置Cors服务
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureCorsServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        }
        /// <summary>
        /// 配置Cors
        /// </summary>
        /// <param name="app"></param>
        protected virtual void ConfigureCors(IApplicationBuilder app)
        {
            app.UseCors("AllowAll");
        }
        #endregion
        #region 内存缓存
        /// <summary>
        /// 配置缓存服务
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureCacheManagerServices(IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
        #endregion
        #region Consul
        /// <summary>
        /// 配置Consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceType"></param>
        protected virtual void ConfigureConsulServices(IServiceCollection services, ServiceType serviceType)
        {
            ConsulManage.Init(serviceType);
            ConsulManage.RegisterConsul();
        }
        #endregion
        #region TFMS
        /// <summary>
        /// 配置TFMS服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tfmsConfig"></param>
        protected virtual void ConfigureTFMSServices(IServiceCollection services, TFMSConfigModel tfmsConfig)
        {
            services.AddEventConnectionFactory(tfmsConfig);
            services.AddEventBus(tfmsConfig.QueueName);
        }
        #endregion
        #endregion
    }
}
