using Materal.MergeBlock.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Materal.MergeBlock.Logger
{
    public class SwaggerModule : MergeBlockModule, IMergeBlockModule
    {
        private const string _configKey = "Swagger";
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            SwaggerConfigModel swaggerConfig = context.Configuration.GetValueObject<SwaggerConfigModel>(_configKey) ?? new SwaggerConfigModel();
            string applicationName = context.Configuration.GetValue(nameof(ApplicationConfigModel.ApplicationName)) ?? "MergeBlockApp";
            context.Services.AddSwaggerGen(config =>
            {
                OpenApiInfo openApiInfo = new()
                {
                    Title = swaggerConfig.Title ?? applicationName,
                    Version = swaggerConfig.Version,
                    Description = swaggerConfig.Description
                };
                config.SwaggerDoc(swaggerConfig.Version, openApiInfo);
                if (swaggerConfig.EnableAuthentication)
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
            await base.OnConfigServiceAsync(context);
        }
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            context.ApplicationBuilder.UseSwagger();
            context.ApplicationBuilder.UseSwaggerUI();
            await base.OnApplicationInitAsync(context);
        }
    }
}
