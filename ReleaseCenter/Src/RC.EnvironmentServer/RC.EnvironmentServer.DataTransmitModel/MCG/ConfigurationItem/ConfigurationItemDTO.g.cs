#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.DataTransmitModel;

namespace RC.EnvironmentServer.DataTransmitModel.ConfigurationItem
{
    /// <summary>
    /// 配置项数据传输模型
    /// </summary>
    public partial class ConfigurationItemDTO : ConfigurationItemListDTO, IDTO
    {
    }
}
