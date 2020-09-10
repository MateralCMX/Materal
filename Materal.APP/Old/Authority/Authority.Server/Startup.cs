using Authority.Common;
using Authority.SqliteEFRepository;
using Materal.APP.Common;
using Materal.APP.Common.Interceptors;
using Materal.APP.EFRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Config;
using NLog.Web;
using System;
using System.IO;

namespace Authority.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.Interceptors.Add<AuthenticationInterceptor>();
            });
            services.AddServerServices();
            #region 跨域
            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
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
            app.UseCors("AllowAll");
            app.UseGrpcWeb(new GrpcWebOptions
            {
                DefaultEnabled = true
            });
            #region 配置Nlog
            NLogBuilder.ConfigureNLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, "NLog.config")).GetCurrentClassLogger();
            LogManager.Configuration.Install(new InstallationContext());
            LogManager.Configuration.Variables["AppName"] = "Authority";
            #endregion
            app.UseEndpoints(endpoints =>
            {
                endpoints.AddGrpcRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
            var dbContextHelper = ApplicationService.GetService<DBContextHelper<AuthorityDBContext>>();
            dbContextHelper.Migrate();
            GrpcServiceHelper.JWTConfig = ApplicationConfig.JWTConfig;
        }
    }
}
