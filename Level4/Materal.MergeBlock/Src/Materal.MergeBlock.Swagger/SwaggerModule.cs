using Materal.MergeBlock.Abstractions.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Materal.MergeBlock.Swagger
{
    /// <summary>
    /// Swagger模块
    /// </summary>
    public class SwaggerModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            SwaggerConfig swaggerConfig = GetSwaggerConfig(context.Configuration);
            if (swaggerConfig.Enable)
            {
                string applicationName = MergeBlockConfig.GetApplicationName(context.Configuration);
                context.Services.AddSwaggerGen(config =>
                {
                    OpenApiInfo openApiInfo = new()
                    {
                        Title = swaggerConfig.Title ?? applicationName,
                        Version = swaggerConfig.Version,
                        Description = swaggerConfig.Description
                    };
                    config.SwaggerDoc(swaggerConfig.Version, openApiInfo);
                    bool EnableAuthentication = swaggerConfig.EnableAuthentication ?? context.ModuleInfos.Any(m => m.ModuleName == "Authorzation");
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
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            IConfiguration configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            SwaggerConfig swaggerConfig = GetSwaggerConfig(configuration);
            if (swaggerConfig.Enable)
            {
                context.ApplicationBuilder.UseSwagger();
                context.ApplicationBuilder.UseSwaggerUI();
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
