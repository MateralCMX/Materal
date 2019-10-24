﻿namespace Materal.MicroFront.Common.Models
{
    public class WebSocketConfigModel
    {
        /// <summary>
        /// 使用Libuv
        /// </summary>
        public bool UserLibuv { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 启用SSL
        /// </summary>
        public bool IsSsl { get; set; }
        /// <summary>
        /// 证书路径
        /// </summary>
        public string CertificatePath { get; set; }
        /// <summary>
        /// 证书密码
        /// </summary>
        public string CertificatePassword { get; set; }
        /// <summary>
        /// 最大消息长度
        /// </summary>
        public int MaxMessageLength { get; set; } = 65535;
        /// <summary>
        /// 不显示命令黑名单
        /// </summary>
        public string[] NotShowCommandBlackList { get; set; }
    }
}
