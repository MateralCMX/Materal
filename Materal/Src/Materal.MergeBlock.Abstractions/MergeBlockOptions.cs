namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock配置
    /// </summary>
    public class MergeBlockOptions : IOptions
    {
        /// <summary>
        /// 配置Key
        /// </summary>
        public const string ConfigKey = "MergeBlock";
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; } = "Materal MergeBlock 应用程序";
        /// <summary>
        /// 是否加载根路径
        /// </summary>
        public bool LoadFromRootPath { get; set; } = true;
        /// <summary>
        /// 是否加载插件目录
        /// </summary>
        public bool LoadFromPlugins { get; set; } = true;
    }
}
