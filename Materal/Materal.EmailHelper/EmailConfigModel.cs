using System;

namespace Materal.EmailHelper
{
    public class EmailConfigModel
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
