﻿namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 控制台目标配置
    /// </summary>
    public class ConsoleLoggerTargetConfig : TargetConfig<ConsoleLoggerWriter>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "Console";
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; set; } = "${DateTime}|${Level}|${CategoryName}|${Scope}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// 颜色组
        /// </summary>
        public ColorsConfig Colors { get; set; } = new();
    }
}