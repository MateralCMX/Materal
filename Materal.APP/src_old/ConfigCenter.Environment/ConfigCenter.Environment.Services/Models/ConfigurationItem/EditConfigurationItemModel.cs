using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.Environment.Services.Models.ConfigurationItem
{
    public class EditConfigurationItemModel: AddConfigurationItemModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
