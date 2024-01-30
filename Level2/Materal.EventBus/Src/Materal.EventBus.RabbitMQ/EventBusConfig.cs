﻿namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// 事件总线配置
    /// </summary>
    public class EventBusConfig
    {
        /// <summary>
        /// 主机
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
        /// 重试间隔时间
        /// </summary>
        public int RetryInterval { get; set; } = 5;
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; } = "MateralEventBusExchange";
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; } = "MateralEventBusQueue";
        /// <summary>
        /// 名称后缀
        /// </summary>
        public string NameSuffix { get; set; } = "_Dev";
        /// <summary>
        /// 是否丢弃错误消息
        /// </summary>
        public bool ErrorMeesageDiscard { get; set; } = false;
        /// <summary>
        /// 获取真实交换机名称
        /// </summary>
        /// <returns></returns>
        public string GetTrueExchangeName() => GetName(ExchangeName);
        /// <summary>
        /// 获取真实队列名称
        /// </summary>
        /// <returns></returns>
        public string GetTrueQueueName() => GetName(QueueName);
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetName(string name) => $"{name}{NameSuffix}";

    }
}
