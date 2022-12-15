﻿namespace Materal.Logger.Models
{
    public class MateralLoggerTargetConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public TargetTypeEnum Type { get; set; } = TargetTypeEnum.Console;
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; set; } = "${DateTime}|${Level}|${CategoryName}:${Message}}";
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 缓冲区数量
        /// </summary>
        public int BufferCount { get; set; } = 100;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string? ConnectionString { get; set; }
        public MateralLoggerColorsConfigModel Colors { get; set; } = new();
    }
}
