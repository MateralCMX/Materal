using System.ComponentModel;

namespace Materal.APP.Enums
{
    public enum ServerCategoryEnum
    {
        /// <summary>
        /// 权限系统
        /// </summary>
        [Description("权限系统")]
        Authority = 0,
        /// <summary>
        /// 配置中心
        /// </summary>
        [Description("配置中心")]
        ConfigCenter = 1,
        /// <summary>
        /// 部署系统
        /// </summary>
        [Description("部署系统")]
        Deploy = 2,
    }
}
