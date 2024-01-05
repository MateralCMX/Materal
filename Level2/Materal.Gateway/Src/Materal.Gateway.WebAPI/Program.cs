using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension;
using Materal.Gateway.OcelotExtension.ExceptionInterceptor;
using Materal.Gateway.Service;
using Materal.Gateway.WebAPI.Filters;
using Materal.Logger;
using Materal.Utils.Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Materal.Gateway.WebAPI
{
    /// <summary>
    /// 应用程序启动类
    /// </summary>
    public class Program
    {
        private static readonly string[] swaggerXMLs = ["Materal.Gateway.WebAPI.xml"];
        /// <summary>
        /// 主入口
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            #region 加载配置文件
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ocelot.json");
            builder.Configuration.AddJsonFile(filePath, false, true); //加载Ocelot配置
            ApplicationConfig.Configuration = builder.Configuration;
            #endregion
            #region DI
            IServiceCollection services = builder.Services;
            services.AddMateralLogger();
            services.AddMateralConsulUtils();
            services.AddControllers(m =>
            {
                m.Filters.Add(new AuthorizeFilter());
                m.Filters.Add<ExceptionFilter>();
                m.SuppressAsyncSuffixInActionNames = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .AddNewtonsoftJson(jsonOptions =>
            {
                jsonOptions.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(WebAPIConfig.JWTConfig.KeyBytes),
                        ValidateIssuer = true,
                        ValidIssuer = WebAPIConfig.JWTConfig.Issuer,
                        ValidateAudience = true,
                        ValidAudience = WebAPIConfig.JWTConfig.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(WebAPIConfig.JWTConfig.ExpiredTime)
                    };
                });
            services.AddSwaggerForOcelot(builder.Configuration, m =>
            {
                m.GenerateDocsDocsForGatewayItSelf(opt =>
                {
                    opt.FilePathsForXmlComments = swaggerXMLs;
                    opt.GatewayDocsTitle = "Materal.Gateway.WebAPI";
                    opt.GatewayDocsOpenApiInfo = new()
                    {
                        Title = "Materal.Gateway.WebAPI.WebAPI",
                        Version = "v1",
                        Description = "提供WebAPI接口",
                        Contact = new OpenApiContact { Name = "Materal", Email = "cloomcmx1554@hotmail.com" }
                    };
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
                    opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, bearerScheme);
                    OpenApiSecurityRequirement openApiSecurityRequirement = new()
                    {
                        {bearerScheme , new List<string>()}
                    };
                    opt.AddSecurityRequirement(openApiSecurityRequirement);
                });
            });
            services.TryAddSingleton<IExceptionInterceptor, MateralGatewayExceptionInterceptor>();
            services.AddOcelotGateway();
            services.AddEndpointsApiExplorer();
            #endregion
            WebApplication app = builder.Build();
            await app.UseMateralLoggerAsync();
            #region Init
            IOcelotConfigService ocelotConfigService = app.Services.GetRequiredService<IOcelotConfigService>();
            await ocelotConfigService.InitAsync();
            #endregion
            #region Web
            string managementPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Management");
            DirectoryInfo managementDirectoryInfo = new(managementPath);
            if (!managementDirectoryInfo.Exists)
            {
                managementDirectoryInfo.Create();
                managementDirectoryInfo.Refresh();
            }
            StaticFileOptions staticFileOptions = new()
            {
                FileProvider = new PhysicalFileProvider(managementDirectoryInfo.FullName),
                RequestPath = $"/{managementDirectoryInfo.Name}",
            };
            app.UseStaticFiles(staticFileOptions);
            #endregion
            app.UseCors();
            app.UseAuthentication();
            await app.UseOcelotGatewayAsync(true);
            #region Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerForOcelotUI(m => m.PathToSwaggerGenerator = "/swagger/docs");
            }
            else
            {
                app.UseSwaggerForOcelotUI();
            }
            #endregion
            app.MapControllers();
            await app.RunAsync();
        }
    }
}