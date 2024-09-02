using System.ComponentModel;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 插件类型
    /// </summary>
    [Flags]
    public enum PluginType
    {
        /// <summary>
        /// 模块
        /// </summary>
        [Description("模块")]
        Module = 1,
        /// <summary>
        /// 服务
        /// </summary>
        [Description("服务")]
        Service = 2
    }
}
