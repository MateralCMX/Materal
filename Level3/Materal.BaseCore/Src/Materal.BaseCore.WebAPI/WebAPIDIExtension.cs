﻿using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Common;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.BaseCore.WebAPI.Filters;
using Materal.Logger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Materal.BaseCore.WebAPI
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
        /// <param name="swaggerGenConfig"></param>
        /// <param name="mvcAction"></param>
        /// <param name="otherControlesAssemblys"></param>
        public static IServiceCollection AddWebAPIService(this IServiceCollection services, Action<SwaggerGenOptions> swaggerGenConfig, Action<MvcOptions>? mvcAction, params Assembly[] otherControlesAssemblys)
        {
            services.AddMateralLogger();
            #region MVC
            IMvcBuilder mvcBuild = services.AddControllers(mvcOptions =>
            {
                mvcAction?.Invoke(mvcOptions);
                mvcOptions.Filters.Add(new AuthorizeFilter());
                mvcOptions.Filters.Add<ActionPageQueryFilterAttribute>();
                mvcOptions.Filters.Add<ExceptionFilter>();
                mvcOptions.SuppressAsyncSuffixInActionNames = true;
            })
            .AddApplicationPart(typeof(HealthController).Assembly)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .AddNewtonsoftJson(jsonOptions =>
            {
                jsonOptions.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            foreach (Assembly assembly in otherControlesAssemblys)
            {
                mvcBuild.AddApplicationPart(assembly);
            }
            #endregion
            #region 响应压缩
            services.AddResponseCompression();
            #endregion
            #region 鉴权
            if (WebAPIConfig.EnableAuthentication)
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                    {
                        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(MateralCoreConfig.JWTConfig.KeyBytes),
                            ValidateIssuer = true,
                            ValidIssuer = MateralCoreConfig.JWTConfig.Issuer,
                            ValidateAudience = true,
                            ValidAudience = MateralCoreConfig.JWTConfig.Audience,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.FromSeconds(MateralCoreConfig.JWTConfig.ExpiredTime)
                        };
                    });
            }
            #endregion
            #region Swagger
            static string GetSwaggerConfigString(string? configString, string defaultString) => string.IsNullOrWhiteSpace(configString) ? defaultString : configString;
            if (WebAPIConfig.SwaggerConfig.Enable)
            {
                services.AddSwaggerGen(config =>
                {
                    config.SwaggerDoc(GetSwaggerConfigString(WebAPIConfig.SwaggerConfig.Version, "v1"), new OpenApiInfo
                    {
                        Title = GetSwaggerConfigString(WebAPIConfig.SwaggerConfig.Title, $"{WebAPIConfig.AppName}.WebAPI"),
                        Version = GetSwaggerConfigString(WebAPIConfig.SwaggerConfig.Version, "v1"),
                        Description = GetSwaggerConfigString(WebAPIConfig.SwaggerConfig.Description, "提供WebAPI接口"),
                        Contact = new OpenApiContact { Name = GetSwaggerConfigString(WebAPIConfig.SwaggerConfig.Author, "Materal"), Email = GetSwaggerConfigString(WebAPIConfig.SwaggerConfig.Email, "cloomcmx1554@hotmail.com") }
                    });
                    if (WebAPIConfig.EnableAuthentication)
                    {
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
                    }
                    swaggerGenConfig?.Invoke(config);
                });
            }
            #endregion
            #region 跨域
            services.AddCors(options =>
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
            services.AddEndpointsApiExplorer();
            return services;
        }
    }
}
