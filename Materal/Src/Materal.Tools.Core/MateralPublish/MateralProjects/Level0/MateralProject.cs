using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.MateralPublish.MateralProjects.Level0
{
    /// <summary>
    /// Materal项目
    /// </summary>
    public class MateralProject(ILoggerFactory? loggerFactory = null) : BaseMateralProject(0, 1, "Materal", loggerFactory)
    {
    }
}
