using Materal.MergeBlock.Abstractions.Config;
using Materal.MergeBlock.Authorization.Abstractions;
using Materal.MergeBlock.Authorization.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Materal.MergeBlock.Authorization
{
    /// <summary>
    /// 鉴权模块
    /// </summary>
    public class AuthorizationModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<AuthorizationConfig>(context.Configuration.GetSection(AuthorizationConfig.ConfigKey));
            context.MvcBuilder?.AddMvcOptions(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
                options.Filters.Add<BindLoginInfoToServiceFilterAttribute>();
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
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            context.ApplicationBuilder.UseAuthorization();
            await base.OnApplicationInitAsync(context);
        }
    }
}
