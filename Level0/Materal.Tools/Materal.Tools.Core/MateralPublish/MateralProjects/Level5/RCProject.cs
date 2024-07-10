using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level5
{
    /// <summary>
    /// RC项目
    /// </summary>
    public class RCProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(5, 1, "RC", loggerFactory)
    {
    }
}
