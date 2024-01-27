namespace Materal.MergeBlock.Abstractions
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
        /// 模块目录
        /// </summary>
        public string[] ModulesDirectories { get; set; } = [];
    }
}
