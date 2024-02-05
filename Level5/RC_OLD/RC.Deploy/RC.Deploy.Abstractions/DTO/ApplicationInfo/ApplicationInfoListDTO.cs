namespace RC.Deploy.Abstractions.DTO.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息列表数据传输模型
    /// </summary>
    public partial class ApplicationInfoListDTO
    {
        /// <summary>
        /// 应用程序状态
        /// </summary>
        public ApplicationStatusEnum ApplicationStatus { get; set; }
        /// <summary>
        /// 应用程序状文本
        /// </summary>
        public string ApplicationStatusTxt => ApplicationStatus.GetDescription();
        /// <summary>
        /// 应用程序类型文本
        /// </summary>
        public string ApplicationTypeTxt => ApplicationType.GetDescription();
    }
}
