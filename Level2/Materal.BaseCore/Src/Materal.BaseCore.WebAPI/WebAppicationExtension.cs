using Materal.Abstractions;
using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Common;
using Materal.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Materal.BaseCore.WebAPI
{
    /// <summary>
    /// WebApplication扩展
    /// </summary>
    public static class WebAppicationExtension
    {
        /// <summary>
        /// WebApplication配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="consulTag">服务发现标签</param>
        /// <returns></returns>
        public static WebApplication WebApplicationConfig(this WebApplication app, string? consulTag = null)
        {
            MateralServices.Services = app.Services;
            app.UseMateralLogger(null, MateralCoreConfig.Configuration);
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next.Invoke();
            });
            if (WebAPIConfig.SwaggerConfig.Enable)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (app.Environment.IsDevelopment() && string.IsNullOrWhiteSpace(WebAPIConfig.BaseUrlConfig.Url))
            {
                WebAPIConfig.BaseUrlConfig.Url = MateralCoreConfig.GetValue("ASPNETCORE_URLS");
            }
            if(WebAPIConfig.BaseUrlConfig.IsSSL)
            {
                app.UseHttpsRedirection();
            }
            if (WebAPIConfig.EnableAuthentication)
            {
                app.UseAuthentication();
            }
            app.MapControllers();
            app.UseCors();
            if (WebAPIConfig.ConsulConfig.Enable)
            {
                Task.Run(() =>
                {
                    List<string> tags = new()
                    {
                        WebAPIConfig.AppTitle
                    };
                    if (!string.IsNullOrWhiteSpace(consulTag))
                    {
                        tags.Add(consulTag);
                    }
                    ConsulManager.Init(tags.ToArray());
                    ConsulManager.RegisterConsul();
                });
            }
            return app;
        }
    }
}
