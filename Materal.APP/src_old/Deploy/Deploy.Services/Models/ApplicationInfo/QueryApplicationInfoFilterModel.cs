using Deploy.Enums;
using Materal.Model;

namespace Deploy.Services.Models.ApplicationInfo
{
    /// <summary>
    /// 查询应用程序信息过滤器模型
    /// </summary>
    public class QueryApplicationInfoFilterModel : FilterModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Contains]
        public string Path { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        [Equal]
        public string MainModule { get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Equal]
        public ApplicationStatusEnum? ApplicationStatus { get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Equal]
        public ApplicationTypeEnum? ApplicationType { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        [Contains]
        public string RunParams { get; set; }
    }
}
