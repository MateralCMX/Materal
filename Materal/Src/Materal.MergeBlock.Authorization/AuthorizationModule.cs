using Materal.MergeBlock.Authorization.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Materal.MergeBlock.Authorization
{
    /// <summary>
    /// 鉴权模块
    /// </summary>
    public class AuthorizationModule() : MergeBlockModule("鉴权模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            IConfigurationSection? section = context.Configuration?.GetSection(AuthorizationOptions.ConfigKey);
            if (section is not null)
            {
                context.Services.Configure<AuthorizationOptions>(section);
            }
            IMvcBuilder? mvcBuilder = context.Services.GetSingletonInstance<IMvcBuilder>();
            mvcBuilder?.AddMvcOptions(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
                options.Filters.Add<SetLoginUserInfoAttribute>();
            });
            AuthorizationOptions config = context.Configuration?.GetConfigItem<AuthorizationOptions>(AuthorizationOptions.ConfigKey) ?? new();
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                TokenValidationParameters tokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(config.KeyBytes),
                    ValidateIssuer = true,
                    ValidIssuer = config.Issuer,
                    ValidateAudience = true,
                    ValidAudience = config.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(config.ExpiredTime)
                };
                options.TokenValidationParameters = tokenValidationParameters;
            });
            context.Services.TryAddSingleton<ITokenService, TokenServiceImpl>();
        }
        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            AdvancedContext advancedContext = context.ServiceProvider.GetRequiredService<AdvancedContext>();
            if (advancedContext.App is not WebApplication webApplication) return;
            webApplication.UseAuthentication();
        }
    }
}
