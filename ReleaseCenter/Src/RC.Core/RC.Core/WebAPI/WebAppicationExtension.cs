using Materal.Common;
using Materal.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using RC.Core.Common;
using RC.Core.WebAPI.Common;

namespace RC.Core.WebAPI
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
            app.UseMateralLogger(null, RCConfig.Configuration);
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next.Invoke();
            });
            app.UseCors("*");
            if (Convert.ToBoolean(WebAPIConfig.EnableSwagger))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (app.Environment.IsDevelopment() && string.IsNullOrWhiteSpace(WebAPIConfig.BaseUrlConfig.Url))
            {
                WebAPIConfig.BaseUrlConfig.Url = RCConfig.GetValue("ASPNETCORE_URLS");
            }
            if(WebAPIConfig.BaseUrlConfig.IsSSL)
            {
                app.UseHttpsRedirection();
            }
            app.UseAuthentication();
            app.MapControllers();
            if (WebAPIConfig.ConsulConfig.Enable)
            {
                Task.Run(() =>
                {
                    List<string> tags = new()
                    {
                        "RC",
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
