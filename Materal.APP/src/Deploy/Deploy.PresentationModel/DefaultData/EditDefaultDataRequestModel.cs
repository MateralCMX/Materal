using System;
using System.ComponentModel.DataAnnotations;

namespace Deploy.PresentationModel.DefaultData
{
    /// <summary>
    /// 修改默认数据请求模型
    /// </summary>
    public class EditDefaultDataRequestModel : AddDefaultDataRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识")]
        public Guid ID { get; set; }
    }
}
