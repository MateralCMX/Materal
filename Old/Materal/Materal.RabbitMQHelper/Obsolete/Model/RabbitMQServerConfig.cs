﻿using Materal.Common;
using System.Text;

namespace Materal.RabbitMQHelper.Model
{
    public class RabbitMQServerConfig : IRabbitMQConfig
    {
        public string HostName { get; set; } = string.Empty;
        public string ExchangeName { get; set; } = string.Empty;
        public ExchangeCategoryEnum ExchangeCategory { get; set; } = ExchangeCategoryEnum.Direct;
        public string ExchangeCategoryString => ExchangeCategory.GetDescription();
        public string QueueName { get; set; } = string.Empty;
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
        public string RoutingKey { get; set; } = string.Empty;
        public Encoding Encoding { get; set; } = Encoding.UTF8;
    }
}
