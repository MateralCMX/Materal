namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 访问日志配置
    /// </summary>
    public class AccessLogOptions : IOptions
    {
        /// <summary>
        /// 最大请求体大小
        /// </summary>
        public int MaxBodySize { get; set; } = 2048;
    }
}
