using Microsoft.Extensions.Configuration;

namespace Materal.Abstractions
{
    /// <summary>
    /// MateralConfig配置
    /// </summary>
    public static class MateralConfig
    {
        /// <summary>
        /// 配置对象
        /// </summary>
        public static IConfiguration? Configuration { get; set; }
        /// <summary>
        /// 分页起始数
        /// </summary>
        public static int PageStartNumber { get; set; } = 0;
    }
}
