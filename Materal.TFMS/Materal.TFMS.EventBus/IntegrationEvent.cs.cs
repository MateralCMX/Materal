using System;
using Newtonsoft.Json;

namespace Materal.TFMS.EventBus
{
    /// <summary>
    /// 集成事件
    /// </summary>
    public abstract class IntegrationEvent
    {
        /// <summary>
        /// 集成事件
        /// </summary>
        protected IntegrationEvent()
        {
            ID = Guid.NewGuid();
            CreateDateTime = DateTime.UtcNow;
        }
        /// <summary>
        /// 集成事件
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <param name="createDateTime">创建时间</param>
        [JsonConstructor]
        protected IntegrationEvent(Guid id, DateTime createDateTime)
        {
            ID = id;
            CreateDateTime = createDateTime;
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        [JsonProperty]
        public Guid ID { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public DateTime CreateDateTime { get; private set; }
    }
}
