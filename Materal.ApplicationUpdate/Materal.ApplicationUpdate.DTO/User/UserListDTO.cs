using System;

namespace Materal.ApplicationUpdate.DTO.User
{
    /// <summary>
    /// 用户列表数据传输模型
    /// </summary>
    public class UserListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
