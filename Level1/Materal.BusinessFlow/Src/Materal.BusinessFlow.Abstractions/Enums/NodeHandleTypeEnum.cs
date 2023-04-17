using System.ComponentModel;

namespace Materal.BusinessFlow.Abstractions
{
    /// <summary>
    /// 节点处理类型枚举
    /// </summary>
    public enum NodeHandleTypeEnum : byte
    {
        /// <summary>
        /// 自动
        /// </summary>
        [Description("自动")]
        Auto = 0,
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]        
        User = 1,
        /// <summary>
        /// 用户组
        /// </summary>
        [Description("用户组")]
        UserGroup = 2,
        /// <summary>
        /// 发起人
        /// </summary>
        [Description("发起人")]
        Initiator
    }
}
