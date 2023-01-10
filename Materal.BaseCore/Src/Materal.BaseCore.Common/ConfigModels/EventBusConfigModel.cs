namespace Materal.BaseCore.Common.ConfigModels
{
    /// <summary>
    /// 事件总线配置
    /// </summary>
    public class EventBusConfigModel
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; } = "127.0.0.1";
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 5672;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "guest";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "guest";
        /// <summary>
        /// 交换器名称
        /// </summary>
        public string ExchangeName { get; set; } = "MateralCoreEventBusExchange";
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; } = 5;
    }
}
