using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.ConfigCenter
{
    /// <summary>
    /// 同步请求模型
    /// </summary>
    public class SyncAllConfigRequestModel
    {
        /// <summary>
        /// 源API
        /// </summary>
        [Required(ErrorMessage = "源API不能为空")]
        public string SourceAPI { get; set; }
        /// <summary>
        /// 目标API
        /// </summary>
        [Required(ErrorMessage = "目标API不能为空")]
        public string[] TargetsAPI { get; set; }
    }
}
