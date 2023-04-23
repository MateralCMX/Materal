using System;

namespace Authority.Service.Model.User
{
    /// <summary>
    /// 更改密码模型
    /// </summary>
    public sealed class ExchangePasswordModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
    }
}
