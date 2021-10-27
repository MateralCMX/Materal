using Deploy.Enums;

namespace Deploy.PresentationModel.ApplicationInfo
{
    /// <summary>
    /// 查询应用程序信息过滤器请求模型
    /// </summary>
    public class QueryApplicationInfoFilterRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        public string MainModule { get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationStatusEnum? ApplicationStatus { get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationTypeEnum? ApplicationType { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string RunParams { get; set; }
    }
}
