using System;
using System.ComponentModel.DataAnnotations;

namespace Deploy.PresentationModel.ApplicationInfo
{
    /// <summary>
    /// 删除文件请求模型
    /// </summary>
    public class DeleteFileRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [Required(ErrorMessage = "文件名称")]
        public string FileName{ get; set; }
    }
}
