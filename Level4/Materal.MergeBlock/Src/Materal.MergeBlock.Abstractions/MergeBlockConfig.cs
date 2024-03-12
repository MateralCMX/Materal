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
        /// Http客户端基础地址
        /// </summary>
        public string BaseUrl { get; set; } = "http://127.0.0.1:5000";
        /// <summary>
        /// HttpBody最大大小
        /// </summary>
        public long? HttpBodyMaxSize { get; set; }
        /// <summary>
        /// 模块路径
        /// </summary>
        public string[] Modules { get; set; } = [];
        /// <summary>
        /// 模块目录
        /// </summary>
        public string[] ModulesDirectories { get; set; } = [];
    }
}
