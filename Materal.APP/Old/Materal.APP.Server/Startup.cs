using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Config;
using NLog.Web;
using System;
using System.IO;

namespace Materal.APP.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddGrpc();
            services.AddServerServices();
            #region 跨域
            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            #endregion
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseGrpcWeb(new GrpcWebOptions());
            #region 配置Nlog
            NLogBuilder.ConfigureNLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, "NLog.config")).GetCurrentClassLogger();
            LogManager.Configuration.Install(new InstallationContext());
            LogManager.Configuration.Variables["AppName"] = "Materal.APP";
            #endregion
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.AddGrpcRoute();
            });
        }
    }
}
