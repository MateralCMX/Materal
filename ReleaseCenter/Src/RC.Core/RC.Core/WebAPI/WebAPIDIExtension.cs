using Materal.Logger;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RC.Core.Common;
using RC.Core.SqlServer;
using RC.Core.WebAPI.Common;
using RC.Core.WebAPI.Controllers;
using RC.Core.WebAPI.Filters;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace RC.Core.WebAPI
{
    /// <summary>
    /// WebAPI依赖注入扩展
    /// </summary>
    public static class WebAPIDIExtension
    {
        /// <summary>
        /// 添加WebAPI服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerXmlPaths"></param>
        /// <param name="dbConfig"></param>
        /// <param name="dbServiceLifetime"></param>
        public static IServiceCollection AddWebAPIServices<T>(this IServiceCollection services, string[] swaggerXmlPaths, SqliteConfigModel dbConfig, ServiceLifetime dbServiceLifetime = ServiceLifetime.Transient)
            where T : DbContext
        {
            MateralLoggerManager.CustomConfig.Add("ApplicationName", WebAPIConfig.AppName);
            services.AddMateralLogger();
            #region MVC
            services.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add(new AuthorizeFilter());
                mvcOptions.Filters.Add<ActionPageQueryFilterAttribute>();
                mvcOptions.Filters.Add<ExceptionFilter>();
                mvcOptions.SuppressAsyncSuffixInActionNames = true;
            })
            .AddApplicationPart(typeof(HealthController).Assembly)
            .AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All);
                jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = new FirstUpperNamingPolicy();
            });
            #endregion
            #region 响应压缩
            services.AddResponseCompression();
            #endregion
            #region 鉴权
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(RCConfig.JWTConfig.KeyBytes),
                        ValidateIssuer = true,
                        ValidIssuer = RCConfig.JWTConfig.Issuer,
                        ValidateAudience = true,
                        ValidAudience = RCConfig.JWTConfig.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(RCConfig.JWTConfig.ExpiredTime)
                    };
                });
            #endregion
            #region Swagger
            if (Convert.ToBoolean(WebAPIConfig.EnableSwagger))
            {
                services.AddSwaggerGen(config =>
                {
                    config.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = $"{WebAPIConfig.AppName}.WebAPI",
                        Version = "v1",
                        Description = "提供WebAPI接口",
                        Contact = new OpenApiContact { Name = "Materal", Email = "cloomcmx1554@hotmail.com" }
                    });
                    OpenApiSecurityScheme bearerScheme = new()
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
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;
                    var defaultWwaggerXmlPaths = new[]
                    {
                        $"{basePath}/RC.Core.xml"
                    };
                    foreach (string path in defaultWwaggerXmlPaths)
                    {
                        config.IncludeXmlComments(path);
                    }
                    if (swaggerXmlPaths != null && swaggerXmlPaths.Length > 0)
                    {
                        foreach (string path in swaggerXmlPaths)
                        {
                            config.IncludeXmlComments(path);
                        }
                    }
                });
            }
            #endregion
            #region 跨域
            services.AddCors(policy =>
            {
                policy.AddPolicy("*", opt => opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
            #endregion
            services.AddDataBaseServices<T>(dbConfig, dbServiceLifetime);
            services.AddEndpointsApiExplorer();
            return services;
        }
    }
}
