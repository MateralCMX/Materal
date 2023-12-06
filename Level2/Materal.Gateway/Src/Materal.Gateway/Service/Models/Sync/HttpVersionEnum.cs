using System.ComponentModel;

namespace Materal.Gateway.Service.Models.Sync
{
    /// <summary>
    /// Http版本枚举
    /// </summary>
    public enum HttpVersionEnum
    {
        /// <summary>
        /// 1.0
        /// </summary>
        [Description("1.0")]
        v1_0 = 0,
        /// <summary>
        /// 1.1
        /// </summary>
        [Description("1.1")]
        v1_1 = 1,
        /// <summary>
        /// 2.0
        /// </summary>
        [Description("2.0")]
        v2_0 = 2
    }
}
