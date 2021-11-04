using Deploy.Enums;
using Materal.APP.Core.Models;

namespace Deploy.Domain
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class ApplicationInfo : BaseDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name{ get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        public string MainModule{ get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string RunParams{ get; set; }
    }
}
