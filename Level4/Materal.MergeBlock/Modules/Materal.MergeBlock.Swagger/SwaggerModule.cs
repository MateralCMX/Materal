using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Materal.MergeBlock.Swagger
{
    /// <summary>
    /// Swagger模块
    /// </summary>
    public class SwaggerModule : MergeBlockWebModule, IMergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SwaggerModule() : base("Swagger模块", "Swagger")
        {
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            SwaggerConfig swaggerConfig = GetSwaggerConfig(context.Configuration);
            IOptionsMonitor<MergeBlockConfig> mergeBlockConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<MergeBlockConfig>>();
            if (swaggerConfig.Enable)
            {
                context.Services.AddSwaggerGen(config =>
                {
                    OpenApiInfo openApiInfo = new()
                    {
                        Title = swaggerConfig.Title ?? mergeBlockConfig.CurrentValue.ApplicationName,
                        Version = swaggerConfig.Version,
                        Description = swaggerConfig.Description
                    };
                    config.SwaggerDoc(swaggerConfig.Version, openApiInfo);
                    bool EnableAuthentication = swaggerConfig.EnableAuthentication ?? MergeBlockHost.ModuleInfos.Any(m => m.Name == "Authorization");
                    if (EnableAuthentication)
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
                        OpenApiSecurityRequirement openApiSecurityRequirement = new() { { bearerScheme, new List<string>() } };
                        config.AddSecurityRequirement(openApiSecurityRequirement);
                    }
                });
            }
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IWebApplicationContext context)
        {
            IConfiguration configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            SwaggerConfig swaggerConfig = GetSwaggerConfig(configuration);
            if (swaggerConfig.Enable)
            {
                context.WebApplication.UseSwagger();
                context.WebApplication.UseSwaggerUI();
            }
            await base.OnApplicationInitAsync(context);
        }
        /// <summary>
        /// 获取Swagger配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static SwaggerConfig GetSwaggerConfig(IConfiguration configuration)
        {
            SwaggerConfig swaggerConfig = configuration.GetValueObject<SwaggerConfig>(SwaggerConfig.ConfigKey) ?? new SwaggerConfig();
            return swaggerConfig;
        }
    }
}
