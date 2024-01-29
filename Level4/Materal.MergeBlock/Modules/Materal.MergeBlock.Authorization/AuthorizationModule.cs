using Materal.MergeBlock.Abstractions.WebModule;
using Materal.MergeBlock.Abstractions.WebModule.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Materal.MergeBlock.Authorization
{
    /// <summary>
    /// 鉴权模块
    /// </summary>
    public class AuthorizationModule : MergeBlockWebModule, IMergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public AuthorizationModule() : base("鉴权模块", "Authorization")
        {

        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public override async Task OnConfigServiceAsync(IWebConfigServiceContext context)
        {
            context.Services.Configure<AuthorizationConfig>(context.Configuration.GetSection(AuthorizationConfig.ConfigKey));
            context.MvcBuilder?.AddMvcOptions(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
            AuthorizationConfig authorizationConfig = context.Configuration.GetValueObject<AuthorizationConfig>(AuthorizationConfig.ConfigKey) ?? throw new MergeBlockException($"未找到鉴权配置[{AuthorizationConfig.ConfigKey}]");
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(authorizationConfig.KeyBytes),
                        ValidateIssuer = true,
                        ValidIssuer = authorizationConfig.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authorizationConfig.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(authorizationConfig.ExpiredTime)
                    };
                });
            context.Services.TryAddSingleton<ITokenService, TokenServiceImpl>();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IWebApplicationContext context)
        {
            context.WebApplication.UseAuthorization();
            await base.OnApplicationInitAsync(context);
        }
    }
}
