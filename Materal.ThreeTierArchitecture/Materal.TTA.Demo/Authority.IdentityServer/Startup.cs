using Common;
using Log.PresentationModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using NLog.Web;
using System.Reflection;

namespace Authority.IdentityServer
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionProcessFilter));
            });
            string migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServerServices(_environment, migrationsAssembly);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            #region 配置Nlog
            NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            LogManager.Configuration.Install(new InstallationContext());
            LogManager.Configuration.Variables["NlogConnectionString"] = ApplicationConfig.LogDB.ConnectionString;
            LogManager.Configuration.Variables["AppName"] = "认证服务器";
            #endregion
        }
    }
}
