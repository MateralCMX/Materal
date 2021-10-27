using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.Project
{
    /// <summary>
    /// 修改项目请求模型
    /// </summary>
    public class EditProjectRequestModel: AddProjectRequestModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
