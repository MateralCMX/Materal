using Authority.SqliteEFRepository;
using Materal.APP.Core;
using Materal.APP.Hubs.Hubs;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authority.Server
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly WebAPIStartupHelper _webAPIStartupHelper;
        /// <summary>
        /// Startup
        /// </summary>
        public Startup()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
            var config = new WebAPIStartupConfig
            {
                AppName = "Authority",
                SwaggerXmlPathArray = new[]
                {
                    $"{basePath}/Authority.Server.xml",
                    $"{basePath}/Authority.PresentationModel.xml",
                    $"{basePath}/Authority.DataTransmitModel.xml"
                }
            };
            _webAPIStartupHelper = new WebAPIStartupHelper(config);
        }
        /// <summary>
        /// ≈‰÷√∑˛ŒÒ
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorityServerServices();
            _webAPIStartupHelper.AddServices(services);
        }
        /// <summary>
        /// ≈‰÷√
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var rewriteOptions = new RewriteOptions();
            rewriteOptions.Add(new RedirectHomeIndexRequests("/swagger/index.html"));
            app.UseRewriter(rewriteOptions);
            _webAPIStartupHelper.Configure(app, env);
            var dbContextHelper = ApplicationData.GetService<DBContextHelper<AuthorityDBContext>>();
            dbContextHelper.Migrate();
            ApplicationData.GetService<IServerHub>();
        }
    }
}