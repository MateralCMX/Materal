using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.Web.Abstractions
{
    /// <summary>
    /// Web配置
    /// </summary>
    public class WebOptions : IOptions
    {
        /// <summary>
        /// 基础地址
        /// </summary>
        public string BaseUrl { get; set; } = "http://localhost:5000";
    }
}
