﻿using Materal.TFMS.EventBus;

namespace Materal.TFMS.Demo.Events
{
    public class Event02 : IntegrationEvent
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
