using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension;
using Materal.Gateway.WebAPI.Filters;
using Materal.Logger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Materal.Gateway.WebAPI
{
    /// <summary>
    /// Ӧ�ó���������
    /// </summary>
    public class Program
    {
        private static readonly string[] swaggerXMLs = ["Materal.Gateway.WebAPI.xml"];
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region ���������ļ�
            builder.Configuration.AddJsonFile("Ocelot.json", false, true); //����Ocelot����
            ApplicationConfig.Configuration = builder.Configuration;
            #endregion
            #region DI
            IServiceCollection services = builder.Services;
            services.AddMateralLogger();
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
                        Description = "�ṩWebAPI�ӿ�",
                        Contact = new OpenApiContact { Name = "Materal", Email = "cloomcmx1554@hotmail.com" }
                    };
                    OpenApiSecurityScheme bearerScheme = new()
                    {
                        Description = "������ͷ������JWT��Ȩ������:Authorization:Bearer {token}",
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
            services.AddOcelotGateway();
            services.AddEndpointsApiExplorer();
            #endregion
            var app = builder.Build();
            await app.UseOcelotGatewayAsync(true);
            app.UseSwaggerForOcelotUI(m => m.PathToSwaggerGenerator = "/swagger/docs");
            app.UseAuthentication();
            app.MapControllers();
            app.UseCors();
            app.Run();
        }
    }
}