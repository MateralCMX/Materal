#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.DataTransmitModel;
using RC.Deploy.Enums;

namespace RC.Deploy.DataTransmitModel.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息数据传输模型
    /// </summary>
    public partial class ApplicationInfoDTO : ApplicationInfoListDTO, IDTO
    {
    }
}
