using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Materal.Logger
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class LoggerDIExtension
    {
        /// <summary>
        /// 使用日志服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMateralLogger(this IApplicationBuilder app, IConfiguration? configuration = null)
        {
            LoggerManager.Init(configuration);
            return app;
        }
    }
}
