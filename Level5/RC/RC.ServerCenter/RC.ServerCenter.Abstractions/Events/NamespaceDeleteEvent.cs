﻿using Materal.TFMS.EventBus;

namespace RC.ServerCenter.Abstractions.Events
{
    /// <summary>
    /// 命名空间删除事件
    /// </summary>
    public class NamespaceDeleteEvent : IntegrationEvent
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid NamespaceID { get; set; }
    }
}