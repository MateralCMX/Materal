﻿namespace Materal.MergeBlock.Swagger
{
    public class SwaggerConfigModel
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public static string ConfigKey { get; } = "Swagger";
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = "v1";
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = "提供WebAPI接口";
        /// <summary>
        /// 是否启用鉴权
        /// </summary>
        public bool EnableAuthentication { get; set; } = true;
    }
}