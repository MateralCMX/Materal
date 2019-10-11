using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    /// <summary>
    /// 更改位序模型
    /// </summary>
    public class ChangeIndexModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 目标唯一标识
        /// </summary>
        [Required(ErrorMessage = "目标唯一标识不能为空")]
        public Guid TargetID { get; set; }
        /// <summary>
        /// 在目标下方
        /// </summary>
        [Required(ErrorMessage = "在目标下方不能为空")]
        public bool BelowTheTarget { get; set; }
    }
}
