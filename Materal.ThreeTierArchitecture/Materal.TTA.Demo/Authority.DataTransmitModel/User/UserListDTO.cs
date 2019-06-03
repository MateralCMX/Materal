using Authority.Common;
using Materal.Common;
using System;

namespace Authority.DataTransmitModel.User
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
        /// 账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 性别字符串
        /// </summary>
        public string SexStr => Sex.GetDescription();
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
}
