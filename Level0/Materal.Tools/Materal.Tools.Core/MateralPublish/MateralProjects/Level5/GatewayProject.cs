using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level5
{
    /// <summary>
    /// 网关
    /// </summary>
    public class GatewayProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(5, 0, "Materal.Gateway", loggerFactory)
    {
    }
}
