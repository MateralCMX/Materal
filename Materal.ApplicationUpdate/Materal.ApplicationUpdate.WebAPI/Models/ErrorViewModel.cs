using System;

namespace Materal.ApplicationUpdate.WebAPI.Models
{
    /// <summary>
    /// 错误视图模型
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 返回标识
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// 显示返回标识
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}