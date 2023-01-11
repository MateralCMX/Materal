using RC.Deploy.Enums;

namespace RC.Deploy.PresentationModel.ApplicationInfo
{
    public partial class QueryApplicationInfoRequestModel
    {
        /// <summary>
        /// 应用程序状态
        /// </summary>
        public ApplicationStatusEnum? ApplicationStatus { get; set; }
    }
}
