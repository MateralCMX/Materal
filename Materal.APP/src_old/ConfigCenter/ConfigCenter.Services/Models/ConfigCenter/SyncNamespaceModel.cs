using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.Services.Models.ConfigCenter
{
    /// <summary>
    /// 同步命名空间请求模型
    /// </summary>
    public class SyncNamespaceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        public ICollection<SyncConfigurationItemModel> ConfigurationItems { get; set; }
    }
}
