using System;
using System.ComponentModel.DataAnnotations;

namespace Deploy.PresentationModel.ApplicationInfo
{
    /// <summary>
    /// 修改应用程序信息请求模型
    /// </summary>
    public class EditApplicationInfoRequestModel : AddApplicationInfoRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识")]
        public Guid ID { get; set; }
    }
}
