using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.Services.Models.Project
{
    public class AddProjectModel
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
