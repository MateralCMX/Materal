using Materal.Common;
using Materal.Gateway.Common;
using Materal.Gateway.Filters;
using Materal.Gateway.OcelotExtension;
using Materal.Logger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Materal.Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                Args = args,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                WebRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "www")
            });
            #region ¼ÓÔØÅäÖÃÎÄ¼þ
            builder.Configuration.AddJsonFile("MateralLogger.json", false, true); //¼ÓÔØMateralLoggerÅäÖÃ
            builder.Configuration.AddJsonFile("Ocelot.json", false, true); //¼ÓÔØOcelotÅäÖÃ
            #endregion
            #region DI
            IServiceCollection services = builder.Services;
            #region ÈÕÖ¾
            services.AddMateralLogger();
            #endregion
            #region MVC
            IMvcBuilder mvcBuild = services.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add(new AuthorizeFilter());
                mvcOptions.Filters.Add<ExceptionFilter>();
                mvcOptions.SuppressAsyncSuffixInActionNames = true;
            })
            .AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All);
                jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = new FirstUpperNamingPolicy();
            });
            #endregion
            #region ÏìÓ¦Ñ¹Ëõ
            services.AddResponseCompression();
            #endregion
            #region ¼øÈ¨
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(ApplicationConfig.JWTConfig.KeyBytes),
                        ValidateIssuer = true,
                        ValidIssuer = ApplicationConfig.JWTConfig.Issuer,
                        ValidateAudience = true,
                        ValidAudience = ApplicationConfig.JWTConfig.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(ApplicationConfig.JWTConfig.ExpiredTime)
                    };
                });
            #endregion
            #region Swagger
            services.AddSwaggerForOcelot(builder.Configuration);
            #endregion
            #region ¿çÓò
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
            #region Íø¹Ø
            services.AddOcelotGatewayAsync();
            #endregion
            services.AddEndpointsApiExplorer();
            #endregion
            builder.Host.UseDefaultServiceProvider(configure =>
            {
                configure.ValidateScopes = false;
            });
            WebApplication app = builder.Build();
            #region WebApplication
            MateralServices.Services = app.Services;
            app.UseMateralLogger(null, ApplicationConfig.Configuration);
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });
            if (app.Environment.IsDevelopment() && string.IsNullOrWhiteSpace(ApplicationConfig.BaseUrlConfig.Url))
            {
                ApplicationConfig.BaseUrlConfig.Url = ApplicationConfig.GetValue("ASPNETCORE_URLS");
            }
            if (ApplicationConfig.BaseUrlConfig.IsSSL)
            {
                app.UseHttpsRedirection();
            }
            app.UseAuthentication();
            app.UseCors();
            app.MapControllers();
            app.Use(async (httpContext, next) =>
            {
                await next.Invoke();
            });
            await app.UseOcelotGateway();
            #endregion
            await app.RunAsync();
        }
    }
}