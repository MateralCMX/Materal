using System;

namespace Materal.RabbitMQHelper.Model
{
    public interface IServiceConfig
    {
        /// <summary>
        /// 主机
        /// </summary>
        string HostName { get; set; }
        /// <summary>
        /// 主机端口
        /// </summary>
        int Port { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        TimeSpan Timeout { get; set; }
    }
}
