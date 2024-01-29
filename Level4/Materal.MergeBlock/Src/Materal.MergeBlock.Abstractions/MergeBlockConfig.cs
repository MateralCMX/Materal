﻿namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock配置项
    /// </summary>
    public class MergeBlockConfig
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; } = "MergeBlockApplication";
        /// <summary>
        /// Http客户端基础地址
        /// </summary>
        public string BaseUrl { get; set; } = "http://127.0.0.1:5000";
        /// <summary>
        /// 模块目录
        /// </summary>
        public string[] ModulesDirectories { get; set; } = [];
    }
}
