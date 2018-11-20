using System;

namespace Materal.ApplicationUpdate.Domain
{
    public class User : EntityModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Token到期时间
        /// </summary>
        public DateTime TokenExpireDate { get; set; }
    }
}
