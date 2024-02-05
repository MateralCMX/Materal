using Materal.EventBus.Abstraction;
using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;

namespace RC.EnvironmentServer.Abstractions.Events
{
    /// <summary>
    /// 同步配置
    /// </summary>
    public class SyncConfigEvent : IEvent
    {
        /// <summary>
        /// 目标源环境
        /// </summary>
        public string[] TargetEnvironments { get; set; } = [];
        /// <summary>
        /// 模式
        /// </summary>
        public SyncModeEnum Mode { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        public List<ConfigurationItemListDTO> ConfigurationItems { get; set; } = [];
    }
}