﻿using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志处理器模型
    /// </summary>
    public class LoggerHandlerModel
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public Exception? Exception { get; set; }
        /// <summary>
        /// 线程ID
        /// </summary>
        public string ThreadID { get; set; } = Environment.CurrentManagedThreadId.ToString();
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string? CategoryName { get; set; }
        /// <summary>
        /// 域
        /// </summary>
        public LoggerScope? MateralLoggerScope { get; set; }
    }
}
