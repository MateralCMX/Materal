#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.PresentationModel;
using RC.Deploy.Enums;

namespace RC.Deploy.PresentationModel.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息修改请求模型
    /// </summary>
    public partial class EditApplicationInfoRequestModel : IEditRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        [Required(ErrorMessage = "主模块为空")]
        public string MainModule { get; set; }  = string.Empty;
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Required(ErrorMessage = "应用程序类型为空")]
        public ApplicationTypeEnum ApplicationType { get; set; } 
        /// <summary>
        /// 增量更新
        /// </summary>
        public bool IsIncrementalUpdating { get; set; } 
        /// <summary>
        /// 运行参数
        /// </summary>
        public string? RunParams { get; set; } 
    }
}
