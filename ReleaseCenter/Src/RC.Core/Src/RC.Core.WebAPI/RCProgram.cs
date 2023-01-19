using Materal.BaseCore.WebAPI;
using Materal.BaseCore.WebAPI.Common;
using Materal.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RC.Core.WebAPI
{
    public abstract class RCProgram : BaseProgram
    {
        public static WebApplication RCStart(string[] args, Action<IServiceCollection>? configService, string consulTag)
        {
            return RCStart(args, configService, null, consulTag);
        }
        public static WebApplication RCStart(string[] args, Action<IServiceCollection>? configService, Action<WebApplication>? configAppAction, string consulTag)
        {
            WebApplication app = Start(args, config =>
            {
                config.AddJsonFile("RCConfig.json", false, true);
            }, configService, configApp =>
            {
                MateralLoggerManager.CustomConfig.Add("ApplicationName", WebAPIConfig.AppName);
                configAppAction?.Invoke(configApp);
            }, consulTag);
            return app;
        }
    }
}
