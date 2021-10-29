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
        /// 项目唯一标识
        /// </summary>
        [Required(ErrorMessage = "项目唯一标识不能为空")]
        public Guid ProjectID { get; set; }
        /// <summary>
        /// 要同步的命名空间
        /// </summary>
        [Required(ErrorMessage = "要同步的命名空间不能为空")]
        public List<SyncNamespaceRequestModel> Namespaces { get; set; }
    }
}
