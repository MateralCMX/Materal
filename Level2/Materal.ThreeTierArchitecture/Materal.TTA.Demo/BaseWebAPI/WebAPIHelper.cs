using System;
using BaseWebAPI.Model;
using Common;
using Log.PresentationModel;
using Materal.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Config;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using Authority.PresentationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace BaseWebAPI
{
    public static class WebAPIHelper
    {
        public static void BaseConfigureServices(IServiceCollection services, BaseConfigureServiceModel baseConfigure)
        {
            #region 配置MVC
            services
                .AddMvc(options =>
                {
                    //AuthorizationPolicy policy = ScopePolicy.Create(ApplicationConfig.IdentityServer.Scope);
                    //options.Filters.Add(new AuthorizeFilter(policy));
                    options.Filters.Add(typeof(ExceptionProcessFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            #endregion
            #region 配置Authentication
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = ApplicationConfig.IdentityServer.Url;
            //        options.RequireHttpsMetadata = false;
            //        options.ApiName = ApplicationConfig.IdentityServer.Scope;
            //        options.ApiSecret = ApplicationConfig.IdentityServer.Secret;
            //        options.EnableCaching = true;
            //        options.CacheDuration = TimeSpan.FromMinutes(10);
            //    });
            #endregion
            #region 帮助文档
            /*Swashbuckle.AspNetCore*/
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc(baseConfigure.AppName, new Info
                {
                    Version = "0.1",
                    Title = baseConfigure.AppName,
                    Description = "提供WebAPI接口",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Materal", Email = "cloomcmx1554@hotmail.com", Url = "" }
                });
                m.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                m.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", Enumerable.Empty<string>()}
                });
                foreach (string path in baseConfigure.SwaggerHelperXmlPathArray)
                {
                    m.IncludeXmlComments(path);
                }
            });
            #endregion
            #region 跨域
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));
            #endregion
            //ServiceProvider provider = services.BuildServiceProvider();
            //AuthorityFilter.ServiceProvider = provider;
        }

        public static void BaseConfigure(IApplicationBuilder app, IHostingEnvironment env, string appName)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseStaticFiles();
            #region 配置Materal
            MateralConfig.PageStartNumber = 1;
            #endregion
            #region 配置Nlog
            NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            LogManager.Configuration.Install(new InstallationContext());
            LogManager.Configuration.Variables["NlogConnectionString"] = ApplicationConfig.LogDB.ConnectionString;
            LogManager.Configuration.Variables["AppName"] = appName;
            #endregion
            #region 配置帮助文档
            app.UseSwagger(c => { c.RouteTemplate = "{documentName}/swagger.json"; })
                .UseSwaggerUI(c => { c.SwaggerEndpoint($"/{appName}/swagger.json", $"{appName}"); });
            #endregion
            #region 配置MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion
        }
    }
}
