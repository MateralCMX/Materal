using RC.Deploy.Enums;

namespace RC.Deploy.DataTransmitModel.ApplicationInfo
{
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
