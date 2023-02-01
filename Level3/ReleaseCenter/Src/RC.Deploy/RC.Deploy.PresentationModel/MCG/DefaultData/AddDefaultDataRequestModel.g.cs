#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.PresentationModel;
using RC.Deploy.Enums;

namespace RC.Deploy.PresentationModel.DefaultData
{
    /// <summary>
    /// 默认数据添加请求模型
    /// </summary>
    public partial class AddDefaultDataRequestModel : IAddRequestModel
    {
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Required(ErrorMessage = "应用程序类型为空")]
        public ApplicationTypeEnum ApplicationType { get; set; } 
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "数据为空")]
        public string Key { get; set; }  = string.Empty;
        /// <summary>
        /// 数据
        /// </summary>
        [Required(ErrorMessage = "数据为空")]
        public string Data { get; set; }  = string.Empty;
    }
}
