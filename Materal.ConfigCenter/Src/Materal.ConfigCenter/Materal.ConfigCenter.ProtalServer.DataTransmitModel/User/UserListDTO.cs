using System;

namespace Materal.ConfigCenter.ProtalServer.DataTransmitModel.User
{
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
    }
}
