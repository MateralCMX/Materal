using System;
using System.ComponentModel.DataAnnotations;

namespace Deploy.Services.Models.ApplicationInfo
{
    /// <summary>
    /// 修改应用程序信息模型
    /// </summary>
    public class EditApplicationInfoModel : AddApplicationInfoModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识")]
        public Guid ID { get; set; }
    }
}
