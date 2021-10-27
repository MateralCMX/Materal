using Materal.APP.Core;
using Materal.APP.WebAPICore.Filters;
using Materal.APP.WebCore.Policies;
using Materal.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Config;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Newtonsoft.Json;

namespace Materal.APP.WebAPICore
{
    /// <summary>
    /// WebAPIStartup帮助类
    /// </summary>
    public class WebAPIStartupHelper
    {
        /// <summary>
        /// 添加Controllers服务时
        /// </summary>
        public event Action<MvcOptions> OnAddControllers;
        /// <summary>
        /// 添加Json服务时
        /// </summary>
        public event Action<JsonOptions> OnAddJson;
        /// <summary>
        /// 添加响应压缩服务时
        /// </summary>
        public event Action<ResponseCompressionOptions> OnAddResponseCompression;
        /// <summary>
        /// 添加Jwt配置是
        /// </summary>
        public event Action<JwtBearerOptions> OnAddJwtBearer;
        /// <summary>
        /// 添加其他服务时
        /// </summary>
        public event Action<IServiceCollection> OnAddOtherService;
        /// <summary>
        /// 配置终点时
        /// </summary>
        public event Action<IEndpointRouteBuilder> OnUseEndpoints;
        /// <summary>
        /// 配置项
        /// </summary>
        private readonly WebAPIStartupConfig _config;
        /// <summary>
        /// WebAPIStartup帮助类
        /// </summary>
        /// <param name="config"></param>
        public WebAPIStartupHelper(WebAPIStartupConfig config)
        {
            _config = config;
        }
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void AddServices(IServiceCollection serviceCollection)
        {
            #region 添加控制器服务
            serviceCollection.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add(new AuthorizeFilter());
                mvcOptions.Filters.Add<ExceptionFilter>();
                mvcOptions.SuppressAsyncSuffixInActionNames = true;
                OnAddControllers?.Invoke(mvcOptions);
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All);
                jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = new FirstUpperNamingPolicy();
                OnAddJson?.Invoke(jsonOptions);
            });
            #endregion
            #region 添加响应压缩服务
            if (_config.EnableResponseCompression)
            {
                serviceCollection.AddResponseCompression(responseCompressionOptions =>
                {
                    OnAddResponseCompression?.Invoke(responseCompressionOptions);
                });
            }
            #endregion
            #region 添加鉴权服务
            if (_config.EnableAuthentication)
            {
                serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                        OnAddJwtBearer?.Invoke(jwtBearerOptions);
                    });
            }
            #endregion
            #region 添加Swagger
            if (_config.EnableSwagger)
            {
                /*Swashbuckle.AspNetCore*/
                serviceCollection.AddSwaggerGen(m =>
                {
                    m.SwaggerDoc(_config.AppName, new OpenApiInfo
                    {
                        Version = "0.1",
                        Title = _config.AppName,
                        Description = "提供WebAPI接口",
                        Contact = new OpenApiContact { Name = "Materal", Email = "cloomcmx1554@hotmail.com" }
                    });
                    var bearerScheme = new OpenApiSecurityScheme
                    {
                        Description = "在请求头部加入JWT授权。例子:Authorization: Bearer {token}",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    };
                    m.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, bearerScheme);
                    m.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {bearerScheme, new List<string>()}
                    });
                    if (_config.SwaggerXmlPathArray == null) return;
                    foreach (string path in _config.SwaggerXmlPathArray)
                    {
                        m.IncludeXmlComments(path);
                    }
                });
            }
            #endregion
            #region 添加跨域
            if (_config.EnableCors)
            {
                serviceCollection.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            }
            #endregion
            OnAddOtherService?.Invoke(serviceCollection);
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="webHostEnvironment"></param>
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            #region 启用调试错误页面
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            #endregion
            #region Https重定向
            if (_config.EnableHttpsRedirection)
            {
                applicationBuilder.UseHttpsRedirection();
            }
            #endregion
            #region 路由
            applicationBuilder.UseRouting();
            #endregion
            #region 鉴权
            if (_config.EnableAuthentication)
            {
                applicationBuilder.UseAuthentication();
            }
            #endregion
            #region 授权
            if (_config.EnableAuthorization)
            {
                applicationBuilder.UseAuthorization();
            }
            #endregion
            #region 跨域
            if (_config.EnableCors)
            {
                applicationBuilder.UseCors("AllowAll");
            }
            #endregion
            #region Swagger
            applicationBuilder.UseSwagger(c => { c.RouteTemplate = "{documentName}/swagger.json"; })
                .UseSwaggerUI(c => { c.SwaggerEndpoint($"/{_config.AppName}/swagger.json", $"{_config.AppName}"); });
            #endregion
            #region NLog
            NLogBuilder.ConfigureNLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, "NLog.config")).GetCurrentClassLogger();
            LogManager.Configuration.Install(new InstallationContext());
            LogManager.Configuration.Variables["AppName"] = _config.AppName;
            #endregion
            #region Materal
            MateralConfig.PageStartNumber = 1;
            #endregion
            applicationBuilder.UseEndpoints(endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapControllers();
                OnUseEndpoints?.Invoke(endpointRouteBuilder);
            });
            ConsoleHelperBase.StartWrite();
        }
    }
}
