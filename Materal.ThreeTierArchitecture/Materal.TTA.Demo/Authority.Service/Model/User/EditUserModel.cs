using System;
namespace Authority.Service.Model.User
{
    /// <summary>
    /// 用户修改模型
    /// </summary>
    public class EditUserModel : AddUserModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
