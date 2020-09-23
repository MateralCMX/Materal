using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.ConfigCenter
{
    /// <summary>
    /// 同步项目请求模型
    /// </summary>
    public class SyncProjectRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public ICollection<SyncNamespaceRequestModel> Namespaces { get; set; }
    }
}
