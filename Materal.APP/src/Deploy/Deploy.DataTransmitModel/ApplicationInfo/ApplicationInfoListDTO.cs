using Deploy.Enums;
using Materal.Common;

namespace Deploy.DataTransmitModel.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息列表数据传输模型
    /// </summary>
    public class ApplicationInfoListDTO : ApplicationInfoDTO
    {
        /// <summary>
        /// 应用程序状态
        /// </summary>
        public ApplicationStatusEnum ApplicationStatus { get; set; }
        /// <summary>
        /// 应用程序状态字符
        /// </summary>
        public string ApplicationStatusString => ApplicationStatus.GetDescription();
        /// <summary>
        /// 类型字符
        /// </summary>
        public string ApplicationTypeString => ApplicationType.GetDescription();
    }
}
