namespace Materal.MergeBlock.Web.Abstractions
{
    /// <summary>
    /// Web配置
    /// </summary>
    public class WebOptions : IOptions
    {
        /// <summary>
        /// 配置Key
        /// </summary>
        public const string ConfigKey = "Web";
        /// <summary>
        /// 基础地址
        /// </summary>
        public string BaseUrl { get; set; } = "http://localhost:5000";
    }
}
