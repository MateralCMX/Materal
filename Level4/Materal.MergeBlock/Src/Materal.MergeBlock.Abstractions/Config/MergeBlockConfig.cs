﻿namespace Materal.MergeBlock.Abstractions.Config
{
    /// <summary>
    /// 应用程序配置模型
    /// </summary>
    public class MergeBlockConfig
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; } = string.Empty;
        /// <summary>
        /// 异常配置
        /// </summary>
        public ExceptionConfig Exception { get; set; } = new();
        /// <summary>
        /// 获得应用程序名称
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string GetApplicationName(IConfiguration configuration)
        {
            string applicationName = configuration.GetValue(nameof(ApplicationName)) ?? "MergeBlockApp";
            return applicationName;
        }
    }
}