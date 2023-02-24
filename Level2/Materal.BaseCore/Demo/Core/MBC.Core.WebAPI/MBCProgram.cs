using Materal.BaseCore.WebAPI;
using Materal.BaseCore.WebAPI.Common;
using Materal.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MBC.Core.WebAPI
{
    public abstract class MBCProgram : BaseProgram
    {
        public static WebApplication MBCStart(string[] args, Action<IServiceCollection>? configService, string consulTag)
        {
            return MBCStart(args, configService, null, consulTag);
        }
        public static WebApplication MBCStart(string[] args, Action<IServiceCollection>? configService, Action<WebApplication>? configAppAction, string consulTag)
        {
            WebApplication app = Start(args, config =>
            {
                config.AddJsonFile("MBCConfig.json", false, true);
            }, configService, configApp =>
            {
                LoggerManager.CustomConfig.Add("ApplicationName", WebAPIConfig.AppName);
                configAppAction?.Invoke(configApp);
            }, consulTag);
            return app;
        }
    }
}
