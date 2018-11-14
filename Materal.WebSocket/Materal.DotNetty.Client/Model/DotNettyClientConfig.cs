using Materal.StringHelper;
using System;
using System.Net;

namespace Materal.DotNetty.Client.Model
{
    public class DotNettyClientConfig
    {
        /// <summary>
        /// Uri生成器
        /// </summary>
        public UriBuilder UriBuilder { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public IPAddress IPAddress => 
            UriBuilder != null && !string.IsNullOrEmpty(UriBuilder.Host) && UriBuilder.Host.IsIPv4() ? 
            IPAddress.Parse(UriBuilder.Host) : 
            null;

        /// <summary>
        /// 使用Libuv
        /// </summary>
        public bool UseLibuv { get; set; }

        /// <summary>
        /// SSL配置
        /// </summary>
        public DotNettyClientSSLConfig SSLConfig { get; set; }
    }

    public class DotNettyClientSSLConfig
    {
        /// <summary>
        /// 使用SSL
        /// </summary>
        public bool UseSSL { get; set; }
        /// <summary>
        /// 证书路径
        /// </summary>
        public string PfxFilePath { get; set; }
        /// <summary>
        /// 证书密码
        /// </summary>
        public string PfxPassword { get; set; }
    }
}
