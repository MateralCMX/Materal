﻿namespace RC.Authority.DataTransmitModel.User
{
    /// <summary>
    /// Token返回数据传输模型
    /// </summary>
    public class LoginResultDTO
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 有效时间
        /// </summary>
        public uint ExpiredTime { get; set; }
    }
}
