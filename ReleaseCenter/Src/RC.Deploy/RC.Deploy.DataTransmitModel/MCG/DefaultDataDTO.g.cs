#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.DataTransmitModel;
using RC.Deploy.Enums;

namespace RC.Deploy.DataTransmitModel.DefaultData
{
    /// <summary>
    /// 默认数据数据传输模型
    /// </summary>
    public partial class DefaultDataDTO : DefaultDataListDTO, IDTO
    {
    }
}
