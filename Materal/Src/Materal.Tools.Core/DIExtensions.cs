using Materal.Tools.Core.ChangeEncoding;
using Materal.Tools.Core.LFConvert;
using Materal.Tools.Core.Logger;
using Materal.Tools.Core.ProjectClear;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtensions
    {
        /// <summary>
        /// 添加Materal工具
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralTools(this IServiceCollection services)
        {
            services.AddSingleton<ILFConvertService, LFConvertService>();
            services.AddSingleton<IChangeEncodingService, ChangeEncodingService>();
            services.AddSingleton<IProjectClearService, ProjectClearService>();
            #region AddLogger
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.Services.AddSingleton<ILoggerProvider, LoggerProvider>();
                builder.Services.AddSingleton<ILoggerListener, LoggerListener>();
            });
            #endregion
            //services.AddSingleton<ILoggerListener, LoggerListener>();
            return services;
        }
    }
}
