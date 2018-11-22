using System;

namespace Materal.ApplicationUpdate.Service.Model.User
{
    /// <summary>
    /// 修改密码模型
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
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
