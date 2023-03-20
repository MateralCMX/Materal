﻿using System.ComponentModel.DataAnnotations;

namespace Materal.BaseCore.Services.Models
{
    /// <summary>
    /// 时间段请求模型
    /// </summary>
    public class TimeQuantumModel
    {
        /// <summary>
        /// 最小时间点
        /// </summary>
        [Required(ErrorMessage = "最小时间点为空")]
        public DateTime MinDateTime { get; set; }
        /// <summary>
        /// 最大时间点
        /// </summary>
        [Required(ErrorMessage = "最大时间点为空")]
        public DateTime MaxDateTime { get; set; }
    }
}
