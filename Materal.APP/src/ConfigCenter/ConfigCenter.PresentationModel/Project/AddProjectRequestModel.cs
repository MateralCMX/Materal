using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.Project
{
    /// <summary>
    /// 添加项目请求模型
    /// </summary>
    public class AddProjectRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空"), StringLength(100, ErrorMessage = "名称长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述不能为空")]
        public string Description { get; set; }
    }
}
