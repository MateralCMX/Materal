using Materal.Logger.Models;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项扩展
    /// </summary>
    public static class LoggerConfigOptionsExtension
    {
        /// <summary>
        /// 添加一个WebSocket输出
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="port"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        public static LoggerConfigOptions AddWebSocketTarget(this LoggerConfigOptions loggerConfigOptions, string name, int port = 5002, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null)
        {
            WebSocketLoggerTargetConfigModel target = new()
            {
                Name = name,
                Port = port
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            if (colors is not null)
            {
                target.Colors = new LoggerColorsConfigModel(colors);
            }
            loggerConfigOptions.AddTarget(target);
            return loggerConfigOptions;
        }
    }
}
