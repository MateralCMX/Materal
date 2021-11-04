using System;
using System.ComponentModel.DataAnnotations;

namespace Deploy.Services.Models.DefaultData
{
    /// <summary>
    /// 修改默认数据模型
    /// </summary>
    public class EditDefaultDataModel : AddDefaultDataModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识")]
        public Guid ID { get; set; }
    }
}
