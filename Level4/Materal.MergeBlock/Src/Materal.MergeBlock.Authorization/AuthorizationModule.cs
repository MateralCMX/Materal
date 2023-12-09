using Materal.MergeBlock.Authorization.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Materal.MergeBlock.Authorization
{
    public class AuthorizationModule : MergeBlockModule, IMergeBlockModule
    {
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<AuthorizationConfigModel>(context.Configuration.GetSection(AuthorizationConfigModel.ConfigKey));
            context.MvcBuilder?.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()));
            AuthorizationConfigModel authorizationConfig = context.Configuration.GetValueObject<AuthorizationConfigModel>(AuthorizationConfigModel.ConfigKey) ?? throw new MergeBlockException($"未找到鉴权配置[{AuthorizationConfigModel.ConfigKey}]");
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
            context.Services.TryAddSingleton<ITokenService, TokenService>();
            await base.OnConfigServiceAsync(context);
        }
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            context.ApplicationBuilder.UseAuthorization();
            await base.OnApplicationInitAsync(context);
        }
    }
}
