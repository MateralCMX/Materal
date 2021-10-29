using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.ConfigCenter
{
    /// <summary>
    /// 同步命名空间请求模型
    /// </summary>
    public class SyncNamespaceRequestModel
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Required(ErrorMessage = "命名空间唯一标识不能为空")]
        public Guid NamespacesID{ get; set; }
        /// <summary>
        /// 配置项键
        /// </summary>
        [Required(ErrorMessage = "配置项键不能为空")]
        public List<string> Keys { get; set; }
    }
}
