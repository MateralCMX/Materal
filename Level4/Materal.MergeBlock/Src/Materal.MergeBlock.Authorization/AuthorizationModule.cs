using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Materal.MergeBlock.Authorization
{
    public class AuthorizationModule : MergeBlockModule, IMergeBlockModule
    {
        private const string _configKey = "Authorization";
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<AuthorizationConfigModel>(context.Configuration.GetSection(_configKey));
            context.MvcBuilder?.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()));
            AuthorizationConfigModel jwtConfigModel = context.Configuration.GetValueObject<AuthorizationConfigModel>(_configKey) ?? throw new InvalidOperationException($"未找到鉴权配置[{_configKey}]");
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtConfigModel.KeyBytes),
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfigModel.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtConfigModel.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(jwtConfigModel.ExpiredTime)
                    };
                });
            await base.OnConfigServiceAsync(context);
        }
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            context.ApplicationBuilder.UseAuthorization();
            await base.OnApplicationInitAsync(context);
        }
    }
}
