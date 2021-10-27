using Deploy.Enums;
using Materal.APP.Core.Models;

namespace Deploy.Domain
{
    /// <summary>
    /// 默认数据
    /// </summary>
    public class DefaultData : BaseDomain
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
}
