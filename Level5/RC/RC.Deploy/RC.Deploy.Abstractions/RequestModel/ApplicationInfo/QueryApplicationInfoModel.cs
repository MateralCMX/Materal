namespace RC.Deploy.Abstractions.RequestModel.ApplicationInfo
{
    /// <summary>
    /// 查询应用程序信息请求模型
    /// </summary>
    public partial class QueryApplicationInfoRequestModel
    {
        /// <summary>
        /// 应用程序状态
        /// </summary>
        public ApplicationStatusEnum? ApplicationStatus { get; set; }
    }
}
