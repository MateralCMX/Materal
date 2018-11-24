using Materal.ApplicationUpdate.DependencyInjection;
using Materal.ApplicationUpdate.SystemLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Materal.ApplicationUpdate.WebAPI
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 配置对象
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 依赖注入配置
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region MVC
            services.AddMvc(options =>
                {
                    //AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                    //    .RequireRole("Bearer")
                    //    .Build();
                    //options.Filters.Add(new MyAuthorizeFilter(policy));
                    options.Filters.Add(typeof(ExceptionProcessFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            #endregion

            #region AddAuthentication
            services.AddAuthentication("Bearer");
            #endregion

            #region 配置
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            #endregion

            #region 缓存

            #region 跨域

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            #endregion
            services.AddMemoryCache();
            #endregion

            #region 跨域
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));
            #endregion
            services.AddBaseServices();
            services.AddFrameWorkServices();
            services.AddAutoMapperServices();

            #region 帮助文档
            /*Swashbuckle.AspNetCore*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "0.1",
                    Title = "Materal应用程序更新系统",
                    Description = "Materal应用程序更新系统WEPAPI",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Materal", Email = "cloomcmx1554@hotmail.com", Url = "" }
                });
                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});
                //c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    {"Bearer", Enumerable.Empty<string>()}
                //});
                string basePath = PlatformServices.Default.Application.ApplicationBasePath;
                c.IncludeXmlComments(Path.Combine(basePath, "Materal.ApplicationUpdate.WebAPI.xml"));
                c.IncludeXmlComments(Path.Combine(basePath, "Materal.ApplicationUpdate.DTO.xml"));
            });
            #endregion
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            NLogHelper.InitialNlog("WEBAPI");
            //app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}");
            });

            #region 配置帮助文档

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicationUpdate API V1.0"); });

            #endregion
        }
    }
}
