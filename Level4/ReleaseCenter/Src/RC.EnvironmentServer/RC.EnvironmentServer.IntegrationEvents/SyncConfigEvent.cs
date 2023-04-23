using Materal.TFMS.EventBus;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.Enums;

namespace XMJ.Authority.IntegrationEvents
{
    /// <summary>
    /// 同步配置
    /// </summary>
    public class SyncConfigEvent : IntegrationEvent
    {
        /// <summary>
        /// 目标源环境
        /// </summary>
        public string[] TargetEnvironments { get; set; } = Array.Empty<string>();
        /// <summary>
        /// 模式
        /// </summary>
        public SyncModeEnum Mode { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        public List<ConfigurationItemListDTO> ConfigurationItems { get; set; } = new();
    }
}