using System.ComponentModel;

namespace Materal.Common
{
    /// <summary>
    /// 返回对象类型
    /// </summary>
    public enum ResultTypeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 1
    }
}
