﻿using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志服务配置
    /// </summary>
    public class LoggerServerConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = false;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 8800;
        /// <summary>
        /// 目标组
        /// </summary>
        public List<string> Targets { get; set; } = new();
        /// <summary>
        /// 最小等级
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LogLevel MinLevel { get; set; } = LogLevel.Error;
        /// <summary>
        /// 最大等级
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LogLevel MaxLevel { get; set; } = LogLevel.Error;
        /// <summary>
        /// 最小等级
        /// </summary>
        public Fleck.LogLevel FleckMinLevel => MinLevel switch
        {
            LogLevel.Debug => Fleck.LogLevel.Debug,
            LogLevel.Warning => Fleck.LogLevel.Warn,
            LogLevel.Error or LogLevel.Critical => Fleck.LogLevel.Error,
            _ => Fleck.LogLevel.Info,
        };
        /// <summary>
        /// 最大等级
        /// </summary>
        public Fleck.LogLevel FleckMaxLevel => MaxLevel switch
        {
            LogLevel.Debug => Fleck.LogLevel.Debug,
            LogLevel.Warning => Fleck.LogLevel.Warn,
            LogLevel.Error or LogLevel.Critical => Fleck.LogLevel.Error,
            _ => Fleck.LogLevel.Info,
        };
    }
}
